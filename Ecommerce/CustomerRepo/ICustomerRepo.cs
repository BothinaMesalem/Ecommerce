using Ecommerce.Models;

namespace Ecommerce.CustomerRepo
{
    public interface ICustomerRepo
    {
        public Task Add(CustomerDto customerDto);

        public Task Update(CustomerDto customerDto,int id);

        public Task<List<User>> GetAll();
    }
}
