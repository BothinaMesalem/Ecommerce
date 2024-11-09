using System.ComponentModel.DataAnnotations;
namespace Ecommerce.CustomerRepo
{
    public class AllCustomerDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [StringLength(100, ErrorMessage = "User Name can't be longer than 100 characters")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

    }
}
