using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Find(string matchcode);
        IUser FindByUserId(string userId);
        void Add(IUser user);
    }
}
