using System.Collections.Generic;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Services
{
    public interface IRentalService
    {
        void RentBook(int studentId, int bookItemId);
        IEnumerable<BookItem> FindRentedBooks(int studentId);
    }
}
