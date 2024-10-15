using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
  

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, ErrorMessage = "Product Name can't be longer than 100 characters")]
        public string ProductName { get; set; }

        [StringLength(500, ErrorMessage = "Product Description can't be longer than 500 characters")]
        public string ProductDescription { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public int Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number")]
        public int Stack_qty { get; set; }

        public byte[] Image { get; set; }

        //public ProductSize Size { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<ProductProductSize> ProductProductSizes { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
