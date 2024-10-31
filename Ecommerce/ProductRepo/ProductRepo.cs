
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecommerce.ProductRepo
{
    public class ProductRepo : IProductRepo
    {
        EcommerceContext ecdb;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepo(EcommerceContext _ecdb, IWebHostEnvironment webHostEnvironment)
        {
            this.ecdb = _ecdb;
            this._webHostEnvironment = webHostEnvironment;
        }
        public async Task<IEnumerable<AllProductDto>> GetALL()
        {
            var products = await ecdb.Products.Include(p => p.ProductProductSizes).ThenInclude(p=>p.ProductSize)
            .ToListAsync();


            var produtdto = products.Select( product => new AllProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Stack_qty = product.Stack_qty,
                Image=product.Image,
                Size = product.ProductProductSizes !=null ?
                product.ProductProductSizes.Where(p=>p.ProductSize !=null).
                Select(ps => ps.ProductSize.Size)
                .ToList()
                :new List<string>()


            }).ToList();

            return produtdto;
           

        }
        public async Task<AllProductDto> GetById(int id)
        {
            var foundProduct = await ecdb.Products.Include(p => p.ProductProductSizes)
            .ThenInclude(ps => ps.ProductSize).FirstOrDefaultAsync(p=>p.ProductId==id);
            var product = new AllProductDto
            {
                ProductId = foundProduct.ProductId,
                ProductName = foundProduct.ProductName,
                Price = foundProduct.Price,
                ProductDescription = foundProduct.ProductDescription,
                Stack_qty = foundProduct.Stack_qty,
                Image = foundProduct.Image,
                Size = foundProduct.ProductProductSizes.Select(ps => ps.ProductSize.Size).ToList()
            };
           return product;
        }

        public async Task Delete(int id)
        {
            var product= await ecdb.Products.FindAsync(id);
            if (product != null)
            {
                 ecdb.Products.Remove(product);
                 await ecdb.SaveChangesAsync();
                
            }
            
        }
        public async Task Add(ProductDto productDto)
        {
            string imagePath = await SavetoFloder(productDto.Image);

          
            var product = new Product
            {
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                Price = productDto.Price,
                Stack_qty = productDto.Stack_qty,
                UserId = productDto.UserId,
                Image = await ConvertImageToByteArray(productDto.Image)
            };

           
            ecdb.Products.Add(product);
            await ecdb.SaveChangesAsync();

            
            foreach (var sizeName in productDto.Size)
            {
               
                var productSize = await ecdb.ProductSize.FirstOrDefaultAsync(s => s.Size == sizeName);

               
                if (productSize == null)
                {
                    productSize = new ProductSize { Size = sizeName };
                    ecdb.ProductSize.Add(productSize);
                    await ecdb.SaveChangesAsync();
                }

               
                var productProductSize = new ProductProductSize
                {
                    ProductId = product.ProductId,
                    ProductSizeId = productSize.PSizeId
                };

                ecdb.ProductProductSize.Add(productProductSize);
            }

          
            await ecdb.SaveChangesAsync();
        }

        private async Task<byte[]> ConvertImageToByteArray(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null; 
        }

        public async Task Update(UpdateProductDto uproductDto,int id)
        {
            var productFound=await ecdb.Products.Include(p => p.ProductProductSizes)
            .ThenInclude(ps => ps.ProductSize).FirstOrDefaultAsync(a=>a.ProductId==id);
            if (productFound != null)
            {

                productFound.ProductName = uproductDto.productName;
                productFound.ProductDescription = uproductDto.productDescription;
                productFound.Price = uproductDto.price;
                productFound.Stack_qty = uproductDto.stack_qty;
                productFound.ProductProductSizes.Clear();
                foreach (var sizeName in uproductDto.size)
                {
                    var productSize = await ecdb.ProductSize.FirstOrDefaultAsync(s => s.Size == sizeName);

                    if (productSize != null)
                    {
                        productFound.ProductProductSizes.Add(new ProductProductSize
                        {
                            ProductId = productFound.ProductId,
                            ProductSizeId = productSize.PSizeId
                        });
                    }
                }
                productFound.Image = await ConvertImageToByteArray(uproductDto.image);
                


                ecdb.Products.Update(productFound);
                await ecdb.SaveChangesAsync();
            }
            

           
        }
        public async Task<string> SavetoFloder(IFormFile Imagefile)
        {
            if(Imagefile !=null  && Imagefile.Length > 0)
            {
                if (_webHostEnvironment.WebRootPath == null)
                {
                    throw new Exception("WebRootPath is not set.");
                }

                var uploadfloder = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");
                if (!Directory.Exists(uploadfloder))
                {
                    Directory.CreateDirectory(uploadfloder);
                }
                var uniqueFile=Guid.NewGuid().ToString()+"_"+Imagefile.FileName;
                var FilePath=Path.Combine(uploadfloder, uniqueFile);
                using(var filestream = new FileStream(FilePath, FileMode.Create))
                {
                    await Imagefile.CopyToAsync(filestream);
                }
                return Path.Combine("Images/products", uniqueFile);
            }
            return null;

        }
        public async Task<List<AllProductDto>> GetALLbysellerId(int id)
        {
            var products = await ecdb.Products.Include(a => a.User).Include(p => p.ProductProductSizes)
            .ThenInclude(ps => ps.ProductSize).Where(a => a.User.Role == UserRole.Seller).Where(a => a.UserId == id).ToListAsync();


            var produtdto =products.Select(product=>new AllProductDto
            {
                ProductId=product.ProductId,
                ProductName= product.ProductName,
                ProductDescription= product.ProductDescription,
                Price= product.Price,
                Stack_qty=product.Stack_qty,
                Image = product.Image,
                Size = product.ProductProductSizes.Select(ps => ps.ProductSize.Size).ToList(),


            }).ToList();

            return produtdto;
        }

        public async Task Editqty(ProductStackqtyDto productStackqtyDto, int id)
        {
            var product = await ecdb.Products.FirstOrDefaultAsync(a => a.ProductId == id);
            if(product != null)
            {
                product.Stack_qty = productStackqtyDto.Stack_qty;
            }
            ecdb.Products.Update(product);
            await  ecdb.SaveChangesAsync();
        }



    }
}
