using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Models
{
    public class AddressContext : DbContext
    {
        public AddressContext(DbContextOptions<AddressContext> options)
            : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
    }
}
