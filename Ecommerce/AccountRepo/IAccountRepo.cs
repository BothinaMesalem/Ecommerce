using Ecommerce.Models;

namespace Ecommerce.AccountRepo
{
    public interface IAccountRepo
    {
        public Task<User> Login(AccountDto accountDto);
    }
}
