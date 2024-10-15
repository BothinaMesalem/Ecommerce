using Ecommerce.Models;

namespace Ecommerce.CustomerRepo
{
    public class CustomerRepo:ICustomerRepo
    {
        EcommerceContext ecdb;
        public CustomerRepo(EcommerceContext _ecdb) 
        {
            ecdb = _ecdb;
        }
        public async Task  Add(CustomerDto customerDto)
        {
            var Customer = new User
            {
                UserName = customerDto.UserName,
                Email = customerDto.Email,
                Password = customerDto.Password,
                Role = UserRole.Customer,

            };
            await ecdb.Users.AddAsync(Customer);
            ecdb.SaveChanges();
        }
        public async Task Update(CustomerDto customerDto,int Id)
        {
            var customer = ecdb.Users.Where(c=>c.Role == UserRole.Customer).FirstOrDefault(c=>c.UserId==Id);
            if (customer != null)
            {
                customer.UserName=customerDto.UserName;
                customer.Email=customerDto.Email;
                customer.Password=customerDto.Password;

            }
            await ecdb.SaveChangesAsync();
        }
        public async Task<List<User>> GetAll()
        {
            var customer = ecdb.Users.Where(c => c.Role == UserRole.Customer).ToList();
            return customer;
        }
    }
}
