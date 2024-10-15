namespace Ecommerce.Models.OrderRepo
{
    public class AllOrderDetailsDto
    {
        public int OrderDetailId { get; set; }
        public decimal OrderPrice { get; set; }

        public int Quantity { get; set; }


        public int ProductId { get; set; }
    }
}
