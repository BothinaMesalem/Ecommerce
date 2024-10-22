 using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required(ErrorMessage = "OrderPrice is required")]
        [Range(0, double.MaxValue, ErrorMessage = "OrderPrice must be a non-negative number")]
        public decimal OrderPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity  must be greater than Zero")]
        public int Quantity { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }


}
