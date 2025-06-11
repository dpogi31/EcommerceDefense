using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EcommerceDefense.Models; // assuming ApplicationUser is here

namespace EcommerceDefense.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add your custom DbSets here in the future
        // public DbSet<Product> Products { get; set; }
    }
}
