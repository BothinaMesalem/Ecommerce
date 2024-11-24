using Ecommerce.Models;
using Ecommerce.PaymentRepo;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepo _paymentRepo;
        private readonly EcommerceContext _context;

        public PaymentController(IPaymentRepo paymentRepo, EcommerceContext context)
        {
            _paymentRepo = paymentRepo;
            _context = context;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            await _paymentRepo.Add(paymentDto);

           
            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                UserId = paymentDto.UserId,
                PaymentDate = DateTime.UtcNow
            };

            var session = _paymentRepo.CreateCheckoutSession(
                payment,
                "http://localhost:4200/Home", // Success URL
                "http://localhost:4200/cancel"   // Cancel URL
            );

            return Ok(new { sessionId = session.Id });
        }
    }
}
