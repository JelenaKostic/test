namespace ECommerceApplication.Models
{
    public class ShoppingCart
    {
        public long ShoppingCartId { get; set; }

        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public long Quantity { get; set; }
    }
}
