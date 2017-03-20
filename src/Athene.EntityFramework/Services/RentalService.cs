using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Abstractions;
using Athene.Abstractions.Models;
using Athene.EntityFramework.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Athene.EntityFramework.Services
{
    // public class RentalService : IRentalService
    // {
    //     private readonly InventoryDbContext _db;
    //     private readonly UserManager<ApplicationUser> _userManager;
    //     private readonly IUserRepository _userReporitory;
    //     private readonly IInventory _inventory;

    //     public RentalService(InventoryDbContext dbContext, 
    //         IUserRepository userRepository,
    //         IInventory inventory,
    //         UserManager<ApplicationUser> userManager)
    //     {
    //         _db = dbContext;
    //         _userReporitory = userRepository;
    //         _inventory = inventory;
    //         _userManager = userManager;
    //     }

    //     void RentInventoryItem(string userId, int[] inventoryItemIds)
    //     {
    //         var items = _db.InventoryItems
    //             .Where(bi => inventoryItemIds.Contains(bi.Id))
    //             .ToArray();
    //         var user = _userManager.FindByIdAsync(userId).Result;
    //         foreach (var item in items) 
    //         {
    //             item.RentedBy = user;
    //             item.RentedAt = DateTime.Now;
    //         }
    //         _db.SaveChanges();
    //     }

    //     IEnumerable<InventoryItem> FindRentedItemsByUser(string userId)
    //     {
    //         var items = _db.InventoryItems
    //             .Include(bi => bi.StockLocation)
    //             .Include(bi => bi.Item)
    //             .Where(bi => bi.RentedBy.Id == userId)
    //             .ToList();

    //         return items;
    //     }
    // }
}
