using Ecommerce.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.AccountRepo
{
    public class AccountRepo:IAccountRepo
    {
        EcommerceContext ecdb;

        public AccountRepo(EcommerceContext _ecdb)
        {
            ecdb = _ecdb;
        }
        public async Task<string> Login(AccountDto accountDto)
        {
            var user = ecdb.Users.FirstOrDefault(a => a.Email == accountDto.Email);
            if (user != null && user.Password == accountDto.Password)
            {

                List<Claim> userdata = new List<Claim>
                {
                    new Claim("email", accountDto.Email),
                    new Claim("role", user.Role.ToString()),
                    new Claim("id",user.UserId.ToString()),
                    new Claim("name",user.UserName)
                };

                string s = "Welcome to my project Bothina Ahmed";
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s));
                var sigcer = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: userdata,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: sigcer
                );

                var stringtoken = new JwtSecurityTokenHandler().WriteToken(token);
                return stringtoken;
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
        }

    }
}
