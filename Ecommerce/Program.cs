
using Ecommerce.AccountRepo;
using Ecommerce.CustomerRepo;
using Ecommerce.Models;
using Ecommerce.Models.OrderRepo;
using Ecommerce.ProductRepo;
using Ecommerce.SellerRepo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EcommerceContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("iticon")));
            builder.Services.AddScoped<IProductRepo, Ecommerce.ProductRepo.ProductRepo>();
            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            builder.Services.AddScoped<ISellerRepo,Ecommerce.SellerRepo.SellerRepo>();
            builder.Services.AddScoped<ICustomerRepo, Ecommerce.CustomerRepo.CustomerRepo>();
            builder.Services.AddScoped<IAccountRepo, Ecommerce.AccountRepo.AccountRepo>();

            builder.Services.AddAuthentication(op => op.DefaultScheme = "myschema")
                .AddJwtBearer("myschema", option =>
                {
                string s = "Welcome to my project Bothina Ahmed";
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s));
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });

                

            var app = builder.Build();
          
           


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //app.UseRouting();
            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
