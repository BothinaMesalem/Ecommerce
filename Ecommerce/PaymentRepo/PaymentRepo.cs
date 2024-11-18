using Ecommerce.Models;
using Stripe.Checkout;
using Stripe;

namespace Ecommerce.PaymentRepo
{
    public class PaymentRepo:IPaymentRepo
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentRepo(IConfiguration configuration)
        {
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
                            Currency = "usd", // Hardcoded, or use a property if Payment includes currency
                            UnitAmount = payment.Amount * 100, // Convert amount to cents
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
    }
}

