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
        public IActionResult CreateCheckoutSession([FromBody] PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the user exists
            var userExists = _context.Users.Any(u => u.UserId == paymentDto.UserId);
            if (!userExists)
            {
                return BadRequest(new { message = "Invalid UserId. User does not exist." });
            }

            // Map DTO to Entity
            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                UserId = paymentDto.UserId,
                PaymentDate = DateTime.UtcNow
            };

            // Save payment details in the database
            _context.payment.Add(payment);
            _context.SaveChanges();

            // Create Stripe checkout session
            var session = _paymentRepo.CreateCheckoutSession(
                payment,
                "http://localhost:4200/success", // Success URL
                "http://localhost:4200/cancel"   // Cancel URL
            );

            return Ok(new { sessionId = session.Id });
        }

    }
}
