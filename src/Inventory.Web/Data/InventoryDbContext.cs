using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Data
{
    public class InventoryDbContext : IdentityDbContext<ApplicationUser>
    {
        public InventoryDbContext() {}

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StockLocation> StockLocations { get; set; }
        public DbSet<BookItem> BookItems { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<StockLocation>()
                .HasKey(s => new { s.Hall, s.Corridor, s.Rack, s.Level, s.Position });
        }
    }
}
