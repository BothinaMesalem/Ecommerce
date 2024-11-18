using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.PaymentRepo
{
    public class PaymentDto
    {
        public int Amount { get; set; }
        public int UserId { get; set; }
    }
}
