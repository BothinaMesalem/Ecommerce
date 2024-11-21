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
        [HttpPost("AddSeller")]
        public async Task<IActionResult> Add(SellerDto sellerDto)
        {
            await sellerRepo.Add(sellerDto);
            return Ok();
        }
        [HttpDelete("DeleteSeller/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await sellerRepo.Delete(id);
            return Ok();
        }
        [HttpGet("GetAllSeller")]

        public async Task<IActionResult> GetALL()
        {
           var sellers= await sellerRepo.GetAll();
            return Ok(sellers) ;
        }

        [HttpPut("EditSellerProfile/{id}")]
        public async Task<IActionResult> Edit(UpdateSellerDto sellerDto,int id)
        {
            await sellerRepo.Update(sellerDto, id);
            return Ok();

        }
        [HttpGet("GetSellerbyId/{id}")]

        public async Task<IActionResult> GetsellerbyId(int id)
        {
           var sellerdata= await sellerRepo.GetSellerbyId(id);
            return Ok(sellerdata);

        }
        [HttpGet("GetCountofSellers")]
        public async Task<IActionResult> GetCountofSeller()
        {
            var SellerCount = await sellerRepo.GetCountSeller();
            return Ok(SellerCount);
        }

    }
}
