using Ecommerce.Models;
using Ecommerce.Models.OrderRepo;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderDetailsRepo
{
    public class OrderDetailRepo:IOrderDetailRepo
    {
        EcommerceContext ecdb;
        public OrderDetailRepo(EcommerceContext _ecdb) { 
            ecdb= _ecdb;
        }
        public async Task Add(OrderDetailDto orderDetailDto)
        {
            var orderdetails = new OrderDetail
            {
                OrderPrice = orderDetailDto.OrderPrice,
                Quantity = orderDetailDto.Quantity,
                ProductId = orderDetailDto.ProductId,
                OrderId = orderDetailDto.OrderId,
            };
             ecdb.OrderDetails.Add(orderdetails);
              await ecdb.SaveChangesAsync();
        }

        public async Task<List<AllOrderDetailDto>> GetbyOrderId(int id)
        {
            var OrderDetails=await ecdb.OrderDetails.Where(a=>a.OrderId==id).ToListAsync();
            var od = OrderDetails.Select(order => new AllOrderDetailDto
            {
                OrderId = order.OrderId,
                OrderDetailId=order.OrderDetailId,
                ProductId=order.ProductId,
                OrderPrice=order.OrderPrice,
                Quantity=order.Quantity,

            }).ToList();

            return od;
        }
        public async Task<List<AllOrderDetailDto>> GetAll()
        {
            var orderdetails= await ecdb.OrderDetails.ToListAsync();
            var ord = orderdetails.Select(order => new AllOrderDetailDto
            {
                OrderId = order.OrderId,
                OrderDetailId = order.OrderDetailId,
                ProductId = order.ProductId,
                OrderPrice = order.OrderPrice,
                Quantity = order.Quantity,

            }).ToList();
            return ord;
        }

    }
}
