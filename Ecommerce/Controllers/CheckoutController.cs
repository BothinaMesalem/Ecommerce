using Ecommerce.CheckoutRepo;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController:ControllerBase
    {
        ICheckoutRepo checkoutRepo;

        public CheckoutController(ICheckoutRepo _checkoutRepo)
        {
            checkoutRepo = _checkoutRepo;
        }
        [HttpPost("AddUserInfo")]
        public async Task<IActionResult> AddUserInfo(CheckoutDto checkoutDto)
        {
            await checkoutRepo.Add(checkoutDto);
            return Ok();

        }
    }
}
