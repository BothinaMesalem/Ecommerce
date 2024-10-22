using Ecommerce.OrderDetailsRepo;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("Api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
     
        private IOrderDetailRepo orderDetailRepo;

        public OrderDetailController(IOrderDetailRepo _orderDetailRepo)
        {
            orderDetailRepo = _orderDetailRepo;
        }
        
        [HttpPost("AddOrder")]
        public async Task<IActionResult> Create(OrderDetailDto orderDetailDto){
            
            await orderDetailRepo.Add(orderDetailDto);
            return Ok(orderDetailDto);
        }
        [HttpGet("GetAllOrderDetails")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await orderDetailRepo.GetAll();
            return Ok(orderDetails);
        }
        [HttpGet("GetALLorderDetailbyOrderId/{id}")]
        public async Task<IActionResult> GetALLorderDetailbyOrderId(int id)
        {
           var orderdetail= await orderDetailRepo.GetbyOrderId(id);
            return Ok(orderdetail);
        }
        [HttpDelete("RemoveOrder")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderDetailRepo.Delete(id);
            return Ok();
        }
        [HttpPut("EditOrder/{id}")]
        public async Task<IActionResult> Update([FromForm]OrderDetailUpDto orderDetailUpDto ,int id)
        {
            await orderDetailRepo.Update(orderDetailUpDto,id);
            return Ok();
        }
        
      
    }
}
