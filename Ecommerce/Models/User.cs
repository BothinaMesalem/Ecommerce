using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public enum UserRole
    {
        Admin ,
        Seller ,
        Customer 
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [StringLength(100, ErrorMessage = "User Name can't be longer than 100 characters")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100,MinimumLength =8, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        public UserRole Role { get; set; }


         public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }


    }
}
