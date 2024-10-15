using Ecommerce.Models;

namespace Ecommerce.SellerRepo
{
    public interface ISellerRepo
    {
        public Task Add(SellerDto sellerDto);
        public Task Delete(int id);

        public Task Update(SellerDto sellerDto,int id);

        public Task<List<User>> GetAll();

    }
}
