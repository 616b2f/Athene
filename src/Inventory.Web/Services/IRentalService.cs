using System.Collections.Generic;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Services
{
    public interface IRentalService
    {
        void RentBook(string userId, int[] bookItemIds);
        IEnumerable<BookItem> FindRentedBooks(string userId);
    }
}
