using Ecommerce.Models;

namespace Ecommerce.CustomerRepo
{
    public interface ICustomerRepo
    {
        public Task Add(CustomerDto customerDto);

        public Task Update(UpdateCustomerDto customerDto,int id);

        public Task<List<AllCustomerDto>> GetAll();
        public  Task<AllCustomerDto> GetCustomerById(int id);

        public Task Delete(int id);
        public Task<int> GetCountCustomer();
        
    }
}
