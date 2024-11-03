namespace Ecommerce.Models.OrderRepo
{
    public class OrderDetailByUserIdDtocs
    {
        public int OrderDetailId { get; set; }
        public decimal OrderPrice { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }


        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public byte[] Image { get; set; }
    }
}
