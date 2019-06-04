using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Athene.Inventory.Data.Services;

namespace Athene.Inventory.Data
{
    public class UnitOfWork : IUnitOfWork<User>
    {
        private readonly InventoryDbContext _dbContext;

        public IInventoryRepository Inventories { get; private set; }
        public IArticleRepository Articles { get; private set; }
        public IBookMetaRepository BookMetas { get; private set; } 
        public IUserRepository<User> Users  { get; private set; }

        public UnitOfWork(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
            Inventories = new InventoryRepository(dbContext);
            Articles = new ArticleRepository(dbContext);
            BookMetas = new BookMetaRepository(dbContext);
            Users = new UserRepository(dbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}