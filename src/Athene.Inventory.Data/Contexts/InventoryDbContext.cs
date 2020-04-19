using Microsoft.EntityFrameworkCore;
using Athene.Inventory.Abstractions.Models;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

namespace Athene.Inventory.Data.Contexts
{
    public class InventoryDbContext : ApiAuthorizationDbContext<User>
    {
        public InventoryDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

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

            builder.Entity<InventoryItem>(opt => 
            {
                opt.HasKey(x => x.Id);
                opt.HasOne(x => x.RentedBy).WithMany().HasForeignKey(x => x.RentedByUserId);
                opt.HasIndex(x => x.Barcode);
                opt.HasIndex(x => x.ExternalId);
                opt.HasIndex(x => x.RentedByUserId);
            });

            builder.Entity<Article>(opt =>
            {
                opt.HasKey(x => x.ArticleId);
                opt.HasMany(x => x.InventoryItems).WithOne(x => x.Article);
                opt.HasDiscriminator<string>("article_type")
                    .HasValue<Book>("book")
                    .HasValue<EBook>("ebook");
                opt.OwnsMany(x => x.Matchcodes, m => 
                {
                    m.WithOwner().HasForeignKey("ArticleId");
                    m.HasKey(x => x.Id);
                    m.HasIndex(x => x.Value);
                });
            });

            builder.Entity<Book>(opt => opt.HasBaseType<Article>());
            builder.Entity<EBook>(opt => opt.HasBaseType<Article>());
        }
    }
}
