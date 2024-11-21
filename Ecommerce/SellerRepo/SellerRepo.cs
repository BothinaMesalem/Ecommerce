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
        public async Task<AllSellerDto> GetSellerbyId(int id)
        {
            var sellers = await ecdb.Users.Where(s => s.Role == UserRole.Seller).FirstOrDefaultAsync(s=>s.UserId==id);
            var sellerDto = new AllSellerDto
            {
                UserId=sellers.UserId,
                UserName = sellers.UserName,
                Email = sellers.Email,
              

            };
                
            return sellerDto;

        }
        public async Task<AllSellerDto> GetAdminbyId(int id)
        {
            var sellers = await ecdb.Users.Where(s => s.Role == UserRole.Admin).FirstOrDefaultAsync(s => s.UserId == id);
            var admindto = new AllSellerDto
            {
                UserId = sellers.UserId,
                UserName = sellers.UserName,
                Email = sellers.Email,
                

            };

            return admindto;
;

        }

        public async Task Update(UpdateSellerDto sellerDto,int id)
        {
            var seller = await ecdb.Users.Where(s => s.Role == UserRole.Seller).FirstOrDefaultAsync(s => s.UserId == id);
            if (seller != null)
            {
                seller.UserName = sellerDto.UserName;
                seller.Email=sellerDto.Email;
            }
            await ecdb.SaveChangesAsync();
        }
        public async Task UpdateAdmin(UpdateSellerDto sellerDto, int id)
        {
            var seller = await ecdb.Users.Where(s => s.Role == UserRole.Admin).FirstOrDefaultAsync(s => s.UserId == id);
            if (seller != null)
            {
                seller.UserName = sellerDto.UserName;
                seller.Email = sellerDto.Email;
            }
            await ecdb.SaveChangesAsync();
        }
        public async Task<int> GetCountSeller()
        {
            var number = await ecdb.Users.Where(s=>s.Role==UserRole.Seller).CountAsync();
            return number;
        }
    }
}
