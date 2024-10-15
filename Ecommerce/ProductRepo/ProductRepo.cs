
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
        public async Task<IEnumerable<Product>> GetALL()
        {
           return await ecdb.Products.ToListAsync();
        }
        public async Task<Product> GetById(int id)
        {
            return await ecdb.Products.FindAsync(id); 
        }

        public async Task<Product> Delete(int id)
        {
            var product= await ecdb.Products.FindAsync(id);
            if (product != null)
            {
                 ecdb.Products.Remove(product);
                 await ecdb.SaveChangesAsync();
                
            }
            return product;
        }
        public async Task Add(ProductDto productDto)
        {
            string imagebath = await SavetoFloder(productDto.Image);
            
            var product = new Product
            {
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                Price = productDto.Price,
                Stack_qty = productDto.Stack_qty,
               
                UserId = productDto.UserId,
                Image = await ConvertImageToByteArray(productDto.Image)
            };
            var productsize = new ProductSize
            {
                Size = productDto.Size,
            };

            
            ecdb.Products.Add(product);
            await ecdb.SaveChangesAsync();
            ecdb.ProductSize.Add(productsize);
            await ecdb.SaveChangesAsync();
            var productProductSize = new ProductProductSize
            {
                ProductId = product.ProductId,
                ProductSizeId = productsize.PSizeId
            };

            ecdb.ProductProductSize.Add(productProductSize);
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

        public async Task Update(ProductDto productDto,int id)
        {
            var productFound=await ecdb.Products.FindAsync(id);
            if (productFound != null)
            {

                productFound.ProductName = productDto.ProductName;
                productFound.ProductDescription = productDto.ProductDescription;
                productFound.Price = productDto.Price;
                productFound.Stack_qty = productDto.Stack_qty;
                  //productFound.Size = productDto.size;
                 productFound.UserId = productDto.UserId;
                productFound.Image = await ConvertImageToByteArray(productDto.Image);
                


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
                //Image = product.Image,
                Sizes = product.ProductProductSizes.Select(ps => ps.ProductSize.Size).ToList(),


            }).ToList();

            return produtdto;
        }


    }
}
