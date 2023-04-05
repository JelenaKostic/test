namespace ECommerceApplication.Models
{
    public class Customer
    {
        public long CustomerId { get; set; }

        public Address? Address { get; set; }
     
        public string? PhoneNumber { get; set; }
    }
}
