namespace Ecommerce.Models.OrderRepo
{
    public interface IOrderRepo
    {
        public Task  Add(OrderDto OrderDto);

        public Task<List<AllOrderbyUserIdDto>> GetbyuserId(int id);

        public Task<List<AllOrderDto>> GetAll();

        public Task Update(OrderDto OrderDto,int id);

        public Task Delete(int id);
        public  Task UpdateQuantity(orderquantityDto orderqtyDto, int id);

        public  Task<int> GetCount(int id);
    }
}
