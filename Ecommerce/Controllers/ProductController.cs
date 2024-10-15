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
        [HttpGet]
        public async Task<IActionResult> getallProducts()
        {
            var products = await ProductRepo.GetALL();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getproductbyId(int id)
        {
            var product = await ProductRepo.GetById(id);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await ProductRepo.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
        {
            await ProductRepo.Add(productDto);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> EditProduct([FromForm] ProductDto productDto,int id)
        {
            await ProductRepo.Update(productDto,id);
            return Ok(); 
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetALLbysellerId(int id)
        {
           var allproduct=  await ProductRepo.GetALLbysellerId(id);
            return Ok(allproduct);
            
        }





    }
}
