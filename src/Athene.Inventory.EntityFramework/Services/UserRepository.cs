using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.EntityFramework.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.EntityFramework.Services
{
    // public class UserRepository : IUserRepository
    // {
    //     private readonly InventoryDbContext _db;
    //     private readonly UserManager<ApplicationUser> _userManager;

    //     public UserRepository(InventoryDbContext dbContext, UserManager<ApplicationUser> userManager)
    //     {
    //         _db = dbContext;
    //         _userManager = userManager;
    //     }

    //     public IEnumerable<IUser> Find(string matchcode)
    //     {
    //         var users = _db.Users
    //             .Include(u => u.Student)
    //                 .ThenInclude(s => s.SchoolClass)
    //                     .ThenInclude(sc => sc.School)
    //             .Where(s =>
    //                 s.Surname.ToLower().Contains(matchcode) ||
    //                 s.Lastname.ToLower().Contains(matchcode) ||
    //                 s.Email.ToLower().Contains(matchcode))
    //             .ToList();
    //         return users;
    //     }

    //     public IUser FindByUserId(string userId)
    //     {
    //         var user = _db.Users
    //             .Include(u => u.Student)
    //                 .ThenInclude(s => s.SchoolClass)
    //                     .ThenInclude(sc => sc.School)
    //             .SingleOrDefault(u => u.Id == userId);
    //         return user;
    //     }

    //     public void Add(IUser user)
    //     {
    //         var appUser = user as ApplicationUser;
    //         if (appUser != null)
    //         {
    //             //TODO replace by UserManager
    //             _db.Users.Add(appUser);
    //             _db.SaveChanges();
    //         }
    //     }
    // }
}
