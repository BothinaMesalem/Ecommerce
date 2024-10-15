using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class ProductSize
    {
        [Key]
        public int PSizeId { get; set; }
        public string Size { get; set; }
    }
}
