using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Models
{
    public class OrderItemContext : DbContext
    {
        public OrderItemContext(DbContextOptions<OrderItemContext> options)
            : base(options)
        {
        }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
