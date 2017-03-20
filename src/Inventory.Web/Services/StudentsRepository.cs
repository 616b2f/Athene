using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Web.Services
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly InventoryDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsRepository(InventoryDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _db = dbContext;
            _userManager = userManager;
        }

        public IEnumerable<ApplicationUser> Find(string matchcode)
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

        public ApplicationUser FindByUserId(string userId)
        {
            var user = _db.Users
                .Include(u => u.Student)
                    .ThenInclude(s => s.SchoolClass)
                        .ThenInclude(sc => sc.School)
                .SingleOrDefault(u => u.Id == userId);
            return user;
        }

        public void Add(ApplicationUser user)
        {
            //TODO replace by UserManager
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}
