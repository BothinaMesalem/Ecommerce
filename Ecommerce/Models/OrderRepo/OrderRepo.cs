using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models.OrderRepo
{
    public class OrderRepo : IOrderRepo
    {
        EcommerceContext ecdb;


        public OrderRepo(EcommerceContext _ecdb)
        {
            this.ecdb = _ecdb;
        }
        public async Task Add(OrderDto orderDto)
        {
            var order = new Order
            {
                Order_date = DateTime.Now,
                Totalamount = orderDto.Totalamount,
                UserId = orderDto.UserId,

            };
            ecdb.Orders.Add(order);
            await ecdb.SaveChangesAsync();

            foreach (var Orderdetail in orderDto.OrderDetails)
            {
                var orderdeatails = new OrderDetail
                {
                    OrderPrice = Orderdetail.OrderPrice,
                    Quantity = Orderdetail.Quantity,
                    ProductId = Orderdetail.ProductId,
                    OrderId = order.OrderId
                };

                ecdb.OrderDetails.Add(orderdeatails);
            }

            await ecdb.SaveChangesAsync();
        }
        public async Task<List<AllOrderDto>> GetbyuserId(int id)
        {
            var orders = await ecdb.Orders.Include(order => order.OrderDetails).Where(order => order.UserId == id).ToListAsync();
            var Orderdtos = orders.Select(or => new AllOrderDto
            {
                OrderId = or.OrderId,
                UserId = or.UserId,
                Totalamount = or.Totalamount,
                Order_date = or.Order_date,
                OrderDetails = or.OrderDetails.Select(ord => new AllOrderDetailsDto
                {

                    OrderDetailId = ord.OrderDetailId,
                    OrderPrice = ord.OrderPrice,
                    Quantity = ord.Quantity,
                    ProductId = ord.ProductId,

                }).ToList()
            }).ToList();
            return Orderdtos;

        }
        public async Task<List<AllOrderDto>> GetAll()
        {
            var Orders = await ecdb.Orders.Include(orders => orders.OrderDetails).ToListAsync();
            var orderDtos = Orders.Select(order => new AllOrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Totalamount = order.Totalamount,
                Order_date = order.Order_date,
                OrderDetails = order.OrderDetails.Select(or => new AllOrderDetailsDto { 
                    OrderDetailId = or.OrderDetailId,
                    OrderPrice = or.OrderPrice,
                    ProductId = or.ProductId,
                    Quantity = or.Quantity,
                }).ToList()
            }).ToList();

            return orderDtos;
        }
        public async Task Update(OrderDto OrderDto, int id)
        {
            var orderFound = await ecdb.Orders.Include(or => or.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == id);
            if (orderFound != null)
            {
                orderFound.Order_date = OrderDto.Order_date;
                orderFound.UserId = OrderDto.UserId;
                orderFound.Totalamount = OrderDto.Totalamount;
                foreach (var order in OrderDto.OrderDetails)
                {
                    var orderdeataial=new OrderDetail
                    {
                        OrderPrice = order.OrderPrice,
                        Quantity = order.Quantity,
                        ProductId = order.ProductId,
                       
                    };
                }
                await ecdb.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
         var order=   await ecdb.Orders.FindAsync(id);
            if (order != null)
            {
                ecdb.Orders.Remove(order);
                await ecdb.SaveChangesAsync();

            }
          
           
           
        }

    }
}
