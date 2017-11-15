using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Find(string matchcode);
        IUser FindByUserId(string userId);
        void Add(IUser user);
    }
}
