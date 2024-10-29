using Ecommerce.Models;
using Ecommerce.ProductRepo;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductRepo ProductRepo;

        public ProductController(IProductRepo productRepo)
        {
            this.ProductRepo = productRepo;
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> getallProducts()
        {
            var products = await ProductRepo.GetALL();
            return Ok(products);
        }
        [HttpGet("GetProductbyId/{id}")]
        public async Task<IActionResult> getproductbyId(int id)
        {
            var product = await ProductRepo.GetById(id);
            return Ok(product);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await ProductRepo.Delete(id);
            return Ok();
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
        {
            try
            {
                await ProductRepo.Add(productDto);
                return Ok("product added successfully");
            }
            catch
            {
                return BadRequest("Interl server error");
            }
        }
        [HttpPut("EditProduct/{id}")]
        public async Task<IActionResult> EditProduct([FromForm] ProductDto productDto,int id)
        {
            await ProductRepo.Update(productDto,id);
            return Ok(); 
        }
        [HttpGet("GetAllProductBySellerId/{id}")]
        public async Task<IActionResult> GetALLbysellerId(int id)
        {
           var allproduct=  await ProductRepo.GetALLbysellerId(id);
            return Ok(allproduct);
            
        }

        [HttpPut("EditQuantity/{id}")]
        public async Task<IActionResult> EditQuantity(ProductStackqtyDto productStackqtyDto,int id)
        {
            await ProductRepo.Editqty(productStackqtyDto,id);
            return Ok();
        }




    }
}
