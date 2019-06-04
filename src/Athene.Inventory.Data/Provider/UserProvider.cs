using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Data.Services
{
    public class UserProvider : IUserProvider<User>
    {
        private readonly InventoryDbContext _db;

        public UserProvider(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<User> FindByMatchcode(string matchcode)
        {
            var users = _db.Users
                .Include(u => u.Student)
                    .ThenInclude(s => s.SchoolClass)
                        .ThenInclude(sc => sc.School)
                .Where(s =>
                    s.Surname.ToLower().Contains(matchcode) ||
                    s.Lastname.ToLower().Contains(matchcode) ||
                    s.Email.ToLower().Contains(matchcode))
                .ToList();
            return users;
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