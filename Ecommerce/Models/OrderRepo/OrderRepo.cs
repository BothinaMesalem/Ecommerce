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
                    Size=Orderdetail.Size,
                    OrderId = order.OrderId
                };

                ecdb.OrderDetails.Add(orderdeatails);
            }

            await ecdb.SaveChangesAsync();
        }
        public async Task<List<AllOrderbyUserIdDto>> GetbyuserId(int id)
        {
            var orders = await ecdb.Orders.Include(order => order.OrderDetails).ThenInclude(order=>order.Product).Where(order => order.UserId == id).ToListAsync();
            var Orderdtos = orders.Select(or => new AllOrderbyUserIdDto
            {
                OrderId = or.OrderId,
                UserId = or.UserId,
                Totalamount = or.Totalamount,
                Order_date = or.Order_date,
                OrderDetail = or.OrderDetails.Select(ord => new  OrderDetailByUserIdDtocs
                {

                    OrderDetailId = ord.OrderDetailId,
                    OrderPrice = ord.OrderPrice,
                    Quantity = ord.Quantity,
                    Size=ord.Size,
                    ProductId = ord.ProductId,
                    ProductName=ord.Product.ProductName,
                    Image=ord.Product.Image


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
                    Size = or.Size,
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
                        Size= order.Size,
                       
                    };
                }
                await ecdb.SaveChangesAsync();
            }
        }
        public async Task UpdateQuantity(orderquantityDto orderqtyDto, int id)
        {
            var orderFound = await ecdb.Orders.Include(or => or.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == id);
            if (orderFound != null)
            {
                foreach (var orderDetailQty in orderqtyDto.OrderDetailqty)
                {
                    var orderDetail = orderFound.OrderDetails
                        .FirstOrDefault(od => od.OrderDetailId == orderDetailQty.OrderDetailId);

                    if (orderDetail != null)
                    {
                        if (orderDetailQty.Quantity >= 0) 
                        {
                            orderDetail.Quantity = orderDetailQty.Quantity;
                        }
                    }
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
        public async Task<List<AllOrderDto>> Getallordertoseller(int sellerId)
        {
            var Orders = await ecdb.Orders.Include(orders => orders.OrderDetails).ThenInclude(o=>o.Product).Where(o=>o.OrderDetails.Any(od=>od.Product.UserId==sellerId)).ToListAsync();
            var orderDtos = Orders.Select(order => new AllOrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Totalamount = order.Totalamount,
                Order_date = order.Order_date,
                OrderDetails = order.OrderDetails.Where(od=>od.Product.UserId==sellerId).Select(or => new AllOrderDetailsDto
                {
                    OrderDetailId = or.OrderDetailId,
                    OrderPrice = or.OrderPrice,
                    ProductId = or.ProductId,
                    Quantity = or.Quantity,
                    Size = or.Size,
                }).ToList()
            }).ToList();

            return orderDtos;
        }

        public async Task<int> GetCount(int id)
        {
          var ordercount= await ecdb.Orders.Where(o => o.UserId == id).CountAsync();
            return ordercount; 
        }

        public async Task<int> GetOrdersCount()
        {
            var orderscount = await ecdb.Orders.CountAsync();
            return orderscount;
        }
        public async Task<int> GetordersCounttoseller(int sellerId)
        {
            var Ordersnumber = await ecdb.Orders.Include(orders => orders.OrderDetails).ThenInclude(o => o.Product).Where(o => o.OrderDetails.Any(od => od.Product.UserId == sellerId)).CountAsync();
            return Ordersnumber;
        }




    }
}
