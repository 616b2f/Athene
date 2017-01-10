using System;
using System.Linq;
using Athene.Inventory.Web.Data;

namespace Athene.Inventory.Web.Services
{
    public class RentalService : IRentalService
    {
        private readonly InventoryDbContext _db;
        public RentalService(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        public void RentBook(int studentId, int bookItemId)
        {
            var bookItem = _db.BookItems.Single(bi => bi.Id == bookItemId);
            var student = _db.Students.Single(s => s.StudentId == studentId);
            bookItem.RentedBy = student;
            bookItem.RentedAt = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
