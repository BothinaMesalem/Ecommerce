namespace Ecommerce.Models.OrderRepo
{
    public class AllOrderDto
    {

        
        public int OrderId { get; set; }

        public decimal Totalamount { get; set; }
        public int UserId { get; set; }

        public DateTime Order_date { get; set; }
        public OrderStatus Status { get; set; }
        public List<AllOrderDetailsDto> OrderDetails { get; set; }
    }
}
