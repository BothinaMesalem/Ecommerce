namespace Ecommerce.Models.OrderRepo
{
    public class AllOrderbyUserIdDto
    {

        public int OrderId { get; set; }

        public decimal Totalamount { get; set; }
        public int UserId { get; set; }

        public DateTime Order_date { get; set; }
        public List<OrderDetailByUserIdDtocs> OrderDetail { get; set; }
    }
}
