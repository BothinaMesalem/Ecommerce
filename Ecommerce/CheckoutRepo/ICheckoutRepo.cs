namespace Ecommerce.CheckoutRepo
{
    public interface ICheckoutRepo
    {
        public Task Add(CheckoutDto checkoutDto);
    }
}
