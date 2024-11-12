using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task Update(UpdateCustomerDto customerDto,int Id)
        {
            var customer = ecdb.Users.Where(c=>c.Role == UserRole.Customer).FirstOrDefault(c=>c.UserId==Id);
            if (customer != null)
            {
                customer.UserName=customerDto.UserName;
                customer.Email=customerDto.Email;

            }
            await ecdb.SaveChangesAsync();
        }
        public async Task<List<AllCustomerDto>> GetAll()
        {
            var customer = ecdb.Users.Where(c => c.Role == UserRole.Customer).ToList();
            var customers = customer.Select(c => new AllCustomerDto
            {

                UserId = c.UserId,
                UserName = c.UserName,
                Email = c.Email,
            }).ToList();
            return customers;
        }
        public async Task<AllCustomerDto> GetCustomerById(int id)
        {
            var customer =await ecdb.Users.Where(c => c.Role == UserRole.Customer).FirstOrDefaultAsync(c => c.UserId == id);
            var customerData = new AllCustomerDto
            {
                UserId=customer.UserId,
                UserName = customer.UserName,
                Email = customer.Email,
                
            };
            return customerData;
        }
        public async Task Delete(int id)
        {
            var customer = await ecdb.Users.Where(c => c.Role == UserRole.Customer).FirstOrDefaultAsync(c => c.UserId == id);
            var cutomerwithorder = await ecdb.Users.Include(u => u.Orders).FirstOrDefaultAsync(a => a.Orders.Any(od => od.UserId == id) );
            if (customer == cutomerwithorder)
            {
                throw new Exception("Can't Delete ");

            }
            else
            {
                ecdb.Users.Remove(customer);
                await ecdb.SaveChangesAsync();
            }
        }
    }
}
