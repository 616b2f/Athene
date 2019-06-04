using System.Collections.Generic;

namespace Athene.Inventory.Abstractions
{
    public interface IUserProvider<TUser>
    {
        IEnumerable<TUser> FindByMatchcode(string matchcode);
        TUser FindByUserId(string userId);
        IEnumerable<TUser> FindByUserIds(IEnumerable<string> userIds);
    }
}