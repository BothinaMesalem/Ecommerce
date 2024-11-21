using Ecommerce.SellerRepo;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController:ControllerBase
    {
        public ISellerRepo sellerRepo;
        public AdminController(ISellerRepo _sellerRepo)
        {
            sellerRepo = _sellerRepo;
        }
        [HttpPut("EditAdminProfile/{id}")]
        public async Task<IActionResult> Edit(UpdateSellerDto adminDto, int id)
        {
            await sellerRepo.UpdateAdmin(adminDto, id);
            return Ok();
        }
        [HttpGet("GetAdminbyId/{id}")]

        public async Task<IActionResult> GetAdminbyId(int id)
        {
            var Admindata = await sellerRepo.GetAdminbyId(id);
            return Ok(Admindata);

        }
    }
}
