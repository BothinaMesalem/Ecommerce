using Ecommerce.SellerRepo;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController:ControllerBase
    {
        public ISellerRepo sellerRepo;

        public SellerController(ISellerRepo _sellerRepo)
        {
            sellerRepo = _sellerRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Add(SellerDto sellerDto)
        {
            await sellerRepo.Add(sellerDto);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await sellerRepo.Delete(id);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
           var sellers= await sellerRepo.GetAll();
            return Ok(sellers) ;
        }
    }
}
