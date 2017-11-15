using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.EntityFramework.Data
{
    // public class InventoryDbContext : IdentityDbContext<ApplicationUser>
    // {
    //     public InventoryDbContext() : base() {}

    //     public InventoryDbContext(DbContextOptions<InventoryDbContext> options) 
    //         : base(options)
    //     {
    //     }

    //     public DbSet<Book> Books { get; set; }
    //     public DbSet<StockLocation> StockLocations { get; set; }
    //     public DbSet<InventoryItem> InventoryItems { get; set; }
    //     public DbSet<Language> Languages { get; set; }
    //     public DbSet<Publisher> Publisher { get; set; }
    //     public DbSet<Author> Authors { get; set; }
    //     public DbSet<Category> Categories { get; set; }
    //     public DbSet<School> Schools { get; set; }

    //     // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     // {
    //     //     options.UseSqlite(Startup.Configuration.GetConnectionString("DefaultConnection"));
    //     // }

    //     protected override void OnModelCreating(ModelBuilder builder)
    //     {
    //         base.OnModelCreating(builder);
    //         // Customize the ASP.NET Identity model and override the defaults if needed.
    //         // For example, you can rename the ASP.NET Identity table names and more.
    //         // Add your customizations after calling base.OnModelCreating(builder);

    //         builder.Entity<StockLocation>(sl => {
    //             sl.HasIndex(s => new { s.Hall, s.Corridor, s.Rack, s.Level, s.Position })
    //               .IsUnique();
    //             sl.HasKey(x => x.Id);
    //         });

    //         builder.Entity<Address>()
    //             .HasKey(x => x.Id);

    //         builder.Entity<Author>()
    //             .HasKey(x => x.Id);

    //         builder.Entity<BoardOfEducation>()
    //             .HasKey(x => x.Id);
            
    //         builder.Entity<School>()
    //             .HasKey(x => x.Id);

    //         builder.Entity<SchoolClass>()
    //             .HasKey(x => x.Id);

    //         builder.Entity<Category>()
    //             .HasKey(x => x.Id);

    //         builder.Entity<Student>(st => {
    //             st.HasKey(x => x.StudentId);
    //             st.Property(x => x.StudentId).ValueGeneratedNever();
    //         });
    //     }
    // }
}
