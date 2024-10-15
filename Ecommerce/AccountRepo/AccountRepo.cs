using Ecommerce.Models;

namespace Ecommerce.AccountRepo
{
    public class AccountRepo:IAccountRepo
    {
        EcommerceContext ecdb;

        public AccountRepo(EcommerceContext _ecdb)
        {
            ecdb = _ecdb;
        }
        public async Task<User> Login(AccountDto accountDto)
        {
            var user= ecdb.Users.FirstOrDefault(a=>a.UserName==accountDto.UserName);
            if (user != null && user.Password==accountDto.Password) 
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
