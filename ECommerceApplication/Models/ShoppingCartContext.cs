using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Models
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
        }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}
