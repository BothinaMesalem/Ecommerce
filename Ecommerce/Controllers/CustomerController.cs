using Ecommerce.CustomerRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        ICustomerRepo customerRepo;
        public CustomerController(ICustomerRepo _customerRepo) {
            customerRepo = _customerRepo;
        }

      
        [HttpPost("UserSignUP")]

        public async Task<IActionResult> CreateUser(CustomerDto customerDto)
        {
            await customerRepo.Add(customerDto);
            return Ok();
        }
        [HttpPut("EditCustomerProfile/{id}")]
        public async Task<IActionResult> EditUser(UpdateCustomerDto customerDto,int id)
        {
            await customerRepo.Update(customerDto,id);
            return Ok();
        }
        [HttpGet("GetAllCustomer")]
     
        public async Task<IActionResult> GetAll()
        {
           var cutomers= await customerRepo.GetAll();
            return Ok(cutomers);
        }

        [HttpGet("GetCustomerdatabyId/{id}")]
        public async Task<IActionResult> GetCustomerdatabyId(int id)
        {
            var cutomer = await customerRepo.GetCustomerById(id);
            return Ok(cutomer);
        }

        [HttpDelete("DeleteUser/{id}")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            await customerRepo.Delete(id);
            return Ok();
        }

    }
}
