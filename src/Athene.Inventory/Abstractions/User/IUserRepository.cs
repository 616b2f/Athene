using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IUserRepository<T> where T : User
    {
        T FindByUserId(string userId);
        IEnumerable<T> FindByUserIds(IEnumerable<string> userIds);
        void Add(T user);
        void AddRange(IEnumerable<T> user);
    }
}
