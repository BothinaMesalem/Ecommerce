using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public  enum OrderStatus
    {
        Pending,
        Delivered,
        Shipped,
    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime Order_date {  get; set; }

        [Required(ErrorMessage = "Totalamount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative number")]
        public decimal Totalamount {  get; set; }

        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
