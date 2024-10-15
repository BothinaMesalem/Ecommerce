using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.OrderRepo
{
    public class OrderDetailDto
    {
        
        public decimal OrderPrice { get; set; }

        public int Quantity { get; set; }


        public int ProductId { get; set; }
    }
}
