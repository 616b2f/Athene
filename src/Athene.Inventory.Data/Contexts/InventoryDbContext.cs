using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Athene.Inventory.Abstractions.Models;
using Microsoft.AspNetCore.Identity;

namespace Athene.Inventory.Data.Contexts
{
    public class InventoryDbContext : IdentityDbContext<User>
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<StockLocation> StockLocations { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<StockLocation>(sl => {
                sl.HasIndex(s => new { s.Hall, s.Corridor, s.Rack, s.Level, s.Position })
                  .IsUnique();
                sl.HasKey(x => x.Id);
            });

            builder.Entity<Address>()
                .HasKey(x => x.Id);

            builder.Entity<Author>()
                .HasKey(x => x.Id);

            builder.Entity<BoardOfEducation>()
                .HasKey(x => x.Id);
            
            builder.Entity<School>()
                .HasKey(x => x.Id);

            builder.Entity<SchoolClass>()
                .HasKey(x => x.Id);

            builder.Entity<Category>()
                .HasKey(x => x.Id);

            builder.Entity<Article>(opt => 
            {
                opt.HasKey(x => x.ArticleId);
                opt.HasMany(x => x.InventoryItems).WithOne(x => x.Article);
                opt.OwnsMany(x => x.Matchcodes, m => 
                {
                    m.HasForeignKey("ArticleId");
                    m.Property<int>("Id");
                    m.HasKey("Id");
                    m.HasIndex(x => x.Value);
                });
            });

            builder.Entity<InventoryItem>(opt => 
            {
                opt.HasKey(x => x.Id);
                opt.HasIndex(x => x.Barcode);
                opt.HasIndex(x => x.ExternalId);
                opt.HasIndex(x => x.RentedByUserId);
                opt.Ignore(x => x.RentedBy);
            });
        }
    }
}
