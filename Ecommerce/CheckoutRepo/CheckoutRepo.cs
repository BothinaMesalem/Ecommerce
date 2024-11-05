using Ecommerce.Models;

namespace Ecommerce.CheckoutRepo
{
    public class CheckoutRepo:ICheckoutRepo
    {
        EcommerceContext ecdb;

        public CheckoutRepo(EcommerceContext _ecdb)
        {
            ecdb = _ecdb;   
        }
        public async Task Add(CheckoutDto checkoutDto)
        {
            var checkout = new Checkout
            {
                Email = checkoutDto.Email,
                FName = checkoutDto.FName,
                LName = checkoutDto.LName,
                City = checkoutDto.City,
                Country =checkoutDto.Country,
                StreetNumberandName = checkoutDto.StreetNumberandName,
                State = checkoutDto.State,
                ZipCode = checkoutDto.ZipCode,
                Info = checkoutDto.Info,
                Phone = checkoutDto.Phone,

            };
            await ecdb.CheckoutDetails.AddAsync(checkout);
            await ecdb.SaveChangesAsync();
        }
    }
}
