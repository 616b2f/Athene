using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IUserRepository
    {
        IEnumerable<IUser> FindByMatchcode(string matchcode);
        IUser FindByUserId(string userId);
        IEnumerable<IUser> FindByUserIds(IEnumerable<string> userIds);
        void Add(IUser user);
    }
}
