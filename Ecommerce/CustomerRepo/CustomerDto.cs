using System.ComponentModel.DataAnnotations;

namespace Ecommerce.CustomerRepo
{
    public class CustomerDto
    {

        [Required(ErrorMessage = "User Name is required")]
        [StringLength(100, ErrorMessage = "User Name can't be longer than 100 characters")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}
