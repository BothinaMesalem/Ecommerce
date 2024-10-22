using System.ComponentModel.DataAnnotations;

namespace Ecommerce.OrderDetailsRepo
{
    public class OrderDetailDto
    {
        
        public decimal OrderPrice { get; set; }

      
        public int Quantity { get; set; }


        public int ProductId { get; set; }

        public int OrderId { get; set; }
    }
}
