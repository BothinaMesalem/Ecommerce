using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.SellerRepo
{
    
    public class SellerRepo:ISellerRepo
    {
        EcommerceContext ecdb;


        public SellerRepo(EcommerceContext _ecdb)
        {
            ecdb = _ecdb;
        }
        public async Task Add(SellerDto sellerDto)
        {
            var Seller = new User
            {
                UserName = sellerDto.UserName,
                Email = sellerDto.Email,
                Password = sellerDto.Password,
                Role = UserRole.Seller
            };
            await  ecdb.Users.AddAsync(Seller);
            ecdb.SaveChanges();
        } 
        public async Task Delete(int id)
        {
            var seller = await ecdb.Users.Where(s=>s.Role==UserRole.Seller).FirstOrDefaultAsync(s=>s.UserId==id);
            if (seller != null)
            {
                 ecdb.Users.Remove(seller);
                await ecdb.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Can't Delete");

            }
        }
        public async Task<List<AllSellerDto>> GetAll()
        {
            var sellers= await ecdb.Users.Where(s=>s.Role==UserRole.Seller).ToListAsync();
            var sellerDto = sellers.Select(seller => new AllSellerDto
            {
                UserId = seller.UserId,
                UserName = seller.UserName,
                Email = seller.Email,

            }
                ).ToList();
            return sellerDto;

        }
        public async Task<SellerDto> GetSellerbyId(int id)
        {
            var sellers = await ecdb.Users.Where(s => s.Role == UserRole.Seller).FirstOrDefaultAsync(s=>s.UserId==id);
            var sellerDto = new SellerDto
            {
                
                UserName = sellers.UserName,
                Email = sellers.Email,
                Password=sellers.Password

            };
                
            return sellerDto;

        }

        public async Task Update(SellerDto sellerDto,int id)
        {
            var seller = await ecdb.Users.Where(s => s.Role == UserRole.Seller).FirstOrDefaultAsync(s => s.UserId == id);
            if (seller != null)
            {
                seller.UserName = sellerDto.UserName;
                seller.Password= sellerDto.Password;
                seller.Email=sellerDto.Email;
            }
            await ecdb.SaveChangesAsync();
        }
    }
}
