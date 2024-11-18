using Ecommerce.Models;
using Stripe;
using Stripe.Checkout;


namespace Ecommerce.PaymentRepo
{
    public interface IPaymentRepo
    {
        public Session CreateCheckoutSession(Payment payment, string successUrl, string cancelUrl);
    }
}
