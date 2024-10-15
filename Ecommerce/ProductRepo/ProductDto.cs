using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ProductRepo
{
    public class ProductDto
    {
       

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, ErrorMessage = "Product Name can't be longer than 100 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        [StringLength(150, ErrorMessage = "Product Description can't be longer than 150 characters")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than Zero")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Stack_qty  is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Stack_qty  must be greater than Zero")]
        public int Stack_qty { get; set; }

        public IFormFile Image { get; set; }

        //public ProductSize size { get; set; }

        public string Size { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
