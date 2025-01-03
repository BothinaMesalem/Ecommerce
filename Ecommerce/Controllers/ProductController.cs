﻿using Ecommerce.Models;
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
        public async Task<IActionResult> EditProduct([FromForm] UpdateProductDto productDto,int id)
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
        [HttpGet("GetAllProductwithSellerName")]
        public async Task<IActionResult> GetAllProductwithSellerName()
        {
            var AllProducts = await ProductRepo.GetAllwithSellerName();
            return Ok(AllProducts);
        }
        [HttpGet("GetFourProducts")]
        public async Task<IActionResult> GetFourProducts()
        {
            var fourProduct = await ProductRepo.getthefourproduct();
            return Ok(fourProduct);
        }
        [HttpGet("GetLastFourProducts")]
        public async Task<IActionResult> GetLastFourProducts()
        {
            var fourProduct = await ProductRepo.getthelastfourproduct();
            return Ok(fourProduct);
        }
        [HttpGet("GetCountOfProducts")]
        public async Task<IActionResult> GetCountOfProductst()
        {
            var number = await ProductRepo.GetCountProducts();
            return Ok(number);
        }

        [HttpGet("GetCountOfProductsbysellerid/{id}")]
        public async Task<IActionResult> GetCountOfProductsbysellerid(int id)
        {
            var number = await ProductRepo.GetCountProductsthataddedbyseller(id);
            return Ok(number);
        }
        [HttpGet("GetCountProductsthatinstockbyseller/{id}")]
        public async Task<IActionResult> GetCountProductsthatinstockbySeller(int id)
        {
            var number = await ProductRepo.GetCountProductsthatinstockbyseller(id);
            return Ok(number);
        }
        [HttpGet("GetCountProductsthatoutstockbyseller/{id}")]
        public async Task<IActionResult> GetCountProductsthatoutstockbySeller(int id)
        {
            var number = await ProductRepo.GetCountProductsthatoutstockbyseller(id);
            return Ok(number);
        }








    }
}
