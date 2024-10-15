namespace Ecommerce.Models
{
    public class ProductProductSize
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }
    }
}
