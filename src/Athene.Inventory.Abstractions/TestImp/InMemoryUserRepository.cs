using System;
using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;
using System.Linq;

namespace Athene.Inventory.Abstractions.TestImp
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<IUser> _user = new List<IUser>();
        public void Add(IUser user)
        {
            _user.Add(user);
        }

        public IEnumerable<IUser> FindByMatchcode(string matchcode)
        {
            var normalizedMatchcode = matchcode.ToLower();
            return _user.Where(u => 
                u.FullName.ToLower().Contains(normalizedMatchcode) ||
                u.StudentId.ToLower().Contains(normalizedMatchcode));
        }

        public IUser FindByUserId(string userId)
        {
            return _user.SingleOrDefault(u => u.Id.ToString() == userId);
        }
    }
}