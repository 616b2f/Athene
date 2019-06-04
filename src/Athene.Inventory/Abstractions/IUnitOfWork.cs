using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IUnitOfWork<TUser> where TUser : IUser
    {
        IInventoryRepository Inventories { get; }
        IArticleRepository Articles { get; }
        IBookMetaRepository BookMetas { get; }
        IUserRepository<TUser> Users { get; }
        void SaveChanges();
    }
}