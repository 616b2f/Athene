using System;
using System.Collections.Generic;
using System.Linq;

namespace Athene.Inventory.Abstractions.TestImp
{
    public class InMemoryUserRepository : IUserRepository<User>
    {
        private readonly List<User> _user = new List<User>();
        public void Add(User user)
        {
            _user.Add(user);
        }

        public void AddRange(IEnumerable<User> users)
        {
            _user.AddRange(users);
        }

        public IEnumerable<User> FindByMatchcode(string matchcode)
        {
            var normalizedMatchcode = matchcode.ToLower();
            return _user.Where(u => 
                u.FullName.ToLower().Contains(normalizedMatchcode) ||
                u.StudentId.ToLower().Contains(normalizedMatchcode));
        }

        public User FindByUserId(string userId)
        {
            return _user.SingleOrDefault(u => u.Id.ToString() == userId);
        }

        public IEnumerable<User> FindByUserIds(IEnumerable<string> userIds)
        {
            return _user.Where(x => userIds.Contains(x.Id));
        }
    }
}