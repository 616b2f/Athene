using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Web.Data;
using Athene.Inventory.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Web.Services
{
    public class RentalService : IRentalService
    {
        private readonly InventoryDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public RentalService(InventoryDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _db = dbContext;
            _userManager = userManager;
        }

        public void RentBook(string userId, int[] bookItemIds)
        {
            var bookItems = _db.BookItems
                .Where(bi => bookItemIds.Contains(bi.Id))
                .ToArray();
            var user = _userManager.FindByIdAsync(userId).Result;
            foreach (var bookItem in bookItems) 
            {
                bookItem.RentedBy = user;
                bookItem.RentedAt = DateTime.Now;
            }
            _db.SaveChanges();
        }

        public IEnumerable<BookItem> FindRentedBooks(string userId)
        {
            var bookItems = _db.BookItems
                .Include(bi => bi.StockLocation)
                .Include(bi => bi.Book)
                .Where(bi => bi.RentedBy.Id == userId)
                .ToList();

            return bookItems;
        }
    }
}
