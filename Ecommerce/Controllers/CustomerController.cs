﻿using Ecommerce.CustomerRepo;
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

      
        [HttpPost("UserSignUP/{id}")]

        public async Task<IActionResult> CreateUser(CustomerDto customerDto)
        {
            await customerRepo.Add(customerDto);
            return Ok();
        }
        [HttpPut("EditCustomerProfile/{id}")]
        public async Task<IActionResult> EditUser(CustomerDto customerDto,int id)
        {
            await customerRepo.Update(customerDto,id);
            return Ok();
        }
        [HttpGet("GetAllCustomer")]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
           var cutomers= await customerRepo.GetAll();
            return Ok(cutomers);
        }
    }
}
