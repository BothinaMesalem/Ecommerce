using Ecommerce.Models;

namespace Ecommerce.SellerRepo
{
    public interface ISellerRepo
    {
        public Task Add(SellerDto sellerDto);
        public Task Delete(int id);

        public Task Update(UpdateSellerDto sellerDto,int id);

        public Task<List<AllSellerDto>> GetAll();
        public Task<AllSellerDto> GetSellerbyId(int id);

        public Task<int> GetCountSeller();

        public  Task UpdateAdmin(UpdateSellerDto sellerDto, int id);

        public Task<AllSellerDto> GetAdminbyId(int id);




    }
}
