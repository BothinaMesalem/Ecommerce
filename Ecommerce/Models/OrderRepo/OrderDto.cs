using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.OrderRepo
{
    public class OrderDto
    {

        //[Key]
        //public int OrderId { get; set; }
       
        public decimal Totalamount { get; set; }
        public int UserId { get; set; }

         public DateTime Order_date { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; } 
    }
}
