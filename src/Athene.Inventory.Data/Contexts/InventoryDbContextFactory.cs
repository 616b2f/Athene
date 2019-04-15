using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Athene.Inventory.Data
{
    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<InventoryDbContext>();
            // builder.user
            // builder.UseSqlite("Data Source=Inventory.db");
            builder.UseMySql("server=localhost;port=3306;database=db;uid=root;password=root");
            return new InventoryDbContext(builder.Options);
            // throw new System.NotImplementedException();
        }
    }
}