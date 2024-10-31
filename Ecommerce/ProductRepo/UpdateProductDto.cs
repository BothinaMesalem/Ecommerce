using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ecommerce.ProductRepo
{
    public class UpdateProductDto
    {

        
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, ErrorMessage = "Product Name can't be longer than 100 characters")]
        public string productName { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        [StringLength(150, ErrorMessage = "Product Description can't be longer than 150 characters")]
        public string productDescription { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than Zero")]
        public int price { get; set; }

        [Required(ErrorMessage = "Stack_qty  is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Stack_qty  must be greater than Zero")]
        public int stack_qty { get; set; }

        public IFormFile image { get; set; }



        public List<string> size { get; set; }
    }
}
