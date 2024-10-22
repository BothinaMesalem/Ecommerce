using Ecommerce.Models.OrderRepo;

namespace Ecommerce.OrderDetailsRepo
{
    public interface IOrderDetailRepo
    {
        public Task Add(OrderDetailDto orderDetailDto);

        public Task<List<AllOrderDetailDto>> GetbyOrderId(int id);

        public Task<List<AllOrderDetailDto>> GetAll();
    }
}
