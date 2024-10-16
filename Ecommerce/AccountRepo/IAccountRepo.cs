using Ecommerce.Models;

namespace Ecommerce.AccountRepo
{
    public interface IAccountRepo
    {
        public Task<string> Login(AccountDto accountDto);
    }
}
