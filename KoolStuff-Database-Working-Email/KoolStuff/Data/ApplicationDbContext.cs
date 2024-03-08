using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KoolStuff.Models;

namespace KoolStuff.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<KoolStuff.Models.Product> Product { get; set; } = default!;
        public DbSet<EmailStore> EmailStore { get; set; }
    }
}
