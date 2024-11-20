using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ecommerce.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
