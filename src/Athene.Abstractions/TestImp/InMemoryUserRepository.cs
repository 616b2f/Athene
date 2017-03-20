using System;
using System.Collections.Generic;
using Athene.Abstractions.Models;
using System.Linq;

namespace Athene.Abstractions.TestImp
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<IUser> _user = new List<IUser>();
        public void Add(IUser user)
        {
            _user.Add(user);
        }

        public IEnumerable<IUser> Find(string matchcode)
        {
            return _user.Where(u => 
                u.FullName.Contains(matchcode) ||
                u.StudentId.Contains(matchcode));
        }

        public IUser FindByUserId(string userId)
        {
            return _user.SingleOrDefault(u => u.Id.ToString() == userId);
        }
    }
}