namespace ECommerceApplication.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public long AvailableQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
