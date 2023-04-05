namespace ECommerceApplication.Models
{
    public class Order
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AppliedDiscount { get; set; }
    }
}
