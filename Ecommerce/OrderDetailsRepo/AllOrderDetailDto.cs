using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.OrderDetailsRepo
{
    public class AllOrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public decimal OrderPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int OrderId { get; set; }
       
    }
}
