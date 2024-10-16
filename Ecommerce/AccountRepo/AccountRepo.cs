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
            var user= ecdb.Users.FirstOrDefault(a=>a.UserName==accountDto.UserName);
            if (user != null && user.Password==accountDto.Password) 
            {
                //return user;
                #region claims
                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim("name",accountDto.UserName));
                #endregion

                #region secretkey
                string s = "Welcome to my project Bothina Ahmed";
                var key=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s));
                #endregion

                var sigcer= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                
                    claims:userdata,
                    expires:DateTime.Now.AddDays(5),
                    signingCredentials:sigcer
                   

                );

                #region convert token to string
                var stringtoken = new JwtSecurityTokenHandler().WriteToken(token);
                #endregion
                return stringtoken;

                
            }
            else
            {
                return null;
            }
            
        }
    }
}
