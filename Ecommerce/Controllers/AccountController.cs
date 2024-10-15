using Ecommerce.AccountRepo;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        IAccountRepo accountRepo;
       public AccountController(IAccountRepo _accountRepo)
        {
            accountRepo = _accountRepo; 
        }
        [HttpPost("Login")]

        public async Task<IActionResult> Login(AccountDto accountDto)
        {
            await accountRepo.Login(accountDto);
            return Ok();
        }
    }
}
