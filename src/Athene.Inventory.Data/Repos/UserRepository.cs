using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Data.Services
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly InventoryDbContext _db;

        public UserRepository(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        public void Add(User user)
        {
            //TODO replace by UserManager
            _db.Users.Add(user);
        }

        public void AddRange(IEnumerable<User> users)
        {
            //TODO replace by UserManager
            _db.Users.AddRange(users);
        }

        public User FindByUserId(string userId)
        {
            var user = _db.Users
                .Include(u => u.Student)
                    .ThenInclude(s => s.SchoolClass)
                        .ThenInclude(sc => sc.School)
                .SingleOrDefault(u => u.Id == userId);
            return user;
        }

        public IEnumerable<User> FindByUserIds(IEnumerable<string> userIds)
        {
            var users = _db.Users
                .Include(u => u.Student)
                    .ThenInclude(s => s.SchoolClass)
                        .ThenInclude(sc => sc.School)
                .Where(u => userIds.Contains(u.Id))
                .ToList();
            return users;
        }
    }
}