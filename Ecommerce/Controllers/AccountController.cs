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
            try
            {
                var token = await accountRepo.Login(accountDto);
                if (token == null)
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

    }
}
