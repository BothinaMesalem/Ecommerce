using Ecommerce.Models.OrderRepo;

using Microsoft.AspNetCore.Mvc;


using System.Threading.Tasks;
namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController:ControllerBase
    {
       
        IOrderRepo orderRepo;

        public OrderController(IOrderRepo _orderRepo)
        {
            orderRepo = _orderRepo;
          
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> Create([FromBody] OrderDto orderDto)
        {
           
                await orderRepo.Add(orderDto);
                return Ok();
           
          
        }

       
        [HttpGet("GetOrderByUserId/{id}")]
        public async Task<IActionResult> GetbyuserId(int id)
        {
          var order=  await orderRepo.GetbyuserId(id);
            return Ok(order);
        }
        [HttpGet("GeTAllOrder")]
        public async Task<IActionResult> GetAll()
        {
            var orders= await orderRepo.GetAll();
           
            return Ok(orders);
        }

        [HttpPut("EditOrder/{id}")]
        public async Task<IActionResult> Update([FromBody] OrderDto orderDto, int id)
        {
            await orderRepo.Update(orderDto,id);
            return Ok();
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderRepo.Delete(id);
            return Ok();
        }
        [HttpPut("EditQuantity/{id}")]

        public async Task<IActionResult> UpdateQuantity(orderquantityDto quantityDto,int id)
        {
            await orderRepo.UpdateQuantity(quantityDto,id);
            return Ok();
        }

        [HttpGet("GetCountByUserId/{id}")]
        public async Task<IActionResult> Getcount(int id)
        {
            var ordercount = await orderRepo.GetCount(id);
            return Ok(ordercount);
        }
        [HttpGet("GetCountofOrders")]
        public async Task<IActionResult> GetCountofOrders()
        {
            var ordersCount= await orderRepo.GetOrdersCount();
            return Ok(ordersCount);
        }
        [HttpGet("getAllOrderToseller/{id}")]
        public async Task<IActionResult> getAllOrderToseller(int id)
        {
            var AllOrders=await orderRepo.Getallordertoseller(id);
            return Ok(AllOrders);
           
        }
        [HttpGet("GetordersCounttoseller/{id}")]
        public async Task<IActionResult> Getorderscounttoseller(int id) 
        { 
            var ordersNumber =await orderRepo.GetordersCounttoseller(id);
            return Ok(ordersNumber);
        }
        [HttpDelete("ASDelete/{id}")]
        public async Task<IActionResult> ASDlete(int id)
        {
            await orderRepo.ASDelete(id);
            return Ok();
        }

    }
}
