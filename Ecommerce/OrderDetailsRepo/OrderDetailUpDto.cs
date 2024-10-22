namespace Ecommerce.OrderDetailsRepo
{
    public class OrderDetailUpDto
    {
        public decimal OrderPrice { get; set; }

        public int Quantity { get; set; }


        public int ProductId { get; set; }
    }
}
