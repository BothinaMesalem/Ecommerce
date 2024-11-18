
using Ecommerce.AccountRepo;
using Ecommerce.CheckoutRepo;
using Ecommerce.CustomerRepo;
using Ecommerce.Models;
using Ecommerce.Models.OrderRepo;
using Ecommerce.OrderDetailsRepo;
using Ecommerce.ProductRepo;
using Ecommerce.SellerRepo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Stripe;
using Ecommerce.PaymentRepo;

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
            builder.Services.AddScoped<IOrderDetailRepo, Ecommerce.OrderDetailsRepo.OrderDetailRepo>();
            builder.Services.AddScoped<ICheckoutRepo,Ecommerce.CheckoutRepo.CheckoutRepo>();
            builder.Services.AddScoped<IPaymentRepo, Ecommerce.PaymentRepo.PaymentRepo>();

            builder.Services.AddAuthentication("myschema")
     .AddJwtBearer("myschema", option =>
     {
         string s = "Welcome to my project Bothina Ahmed"; // Same secret key for verification
         var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s));
         option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             IssuerSigningKey = key // Ensure the same key is used here for verification
         };
     });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


            var app = builder.Build();
          
           


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("AllowSpecificOrigins");
           


            app.MapControllers();

            app.Run();
        }
    }
}
