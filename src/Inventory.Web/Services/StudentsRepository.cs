using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Data;
using Microsoft.AspNetCore.Identity;

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
                .Where(s => 
                        s.Surname.StartsWith(matchcode) ||
                        s.Lastname.StartsWith(matchcode))
                .ToList();
            return users;
        }

        public ApplicationUser FindByUserId(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
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
