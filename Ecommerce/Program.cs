
using Ecommerce.CustomerRepo;
using Ecommerce.Models;
using Ecommerce.Models.OrderRepo;
using Ecommerce.ProductRepo;
using Ecommerce.SellerRepo;
using Microsoft.EntityFrameworkCore;

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

            var app = builder.Build();
          
           


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
