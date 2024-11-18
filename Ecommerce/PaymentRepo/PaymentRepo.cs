using Ecommerce.Models;
using Stripe.Checkout;
using Stripe;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Migrations;

namespace Ecommerce.PaymentRepo
{
    public class PaymentRepo : IPaymentRepo
    {
        EcommerceContext ecdb;
        private readonly StripeSettings _stripeSettings;

        public PaymentRepo(IConfiguration configuration, EcommerceContext _ecdb)
        {
            this.ecdb = _ecdb;
            _stripeSettings = new StripeSettings
            {
                SecretKey = configuration["Stripe:SecretKey"],
                PublishableKey = configuration["Stripe:PublishableKey"]
            };
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public Session CreateCheckoutSession(Payment payment, string successUrl, string cancelUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = payment.Amount * 100, 
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Ecommerce Purchase",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            var service = new SessionService();
            return service.Create(options);
        }

        public async Task Add(PaymentDto paymentDto)
        {
            var user = await ecdb.Users.FirstOrDefaultAsync(u => u.UserId == paymentDto.UserId);
            if (user != null)
            {
                var payment = new Payment
                {
                    Amount = paymentDto.Amount,
                    UserId = paymentDto.UserId,
                    PaymentDate = DateTime.UtcNow
                };

                ecdb.payment.Add(payment);
                await ecdb.SaveChangesAsync(); 
            }
        }
    }
}
