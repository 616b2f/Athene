using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Services
{
    public interface IInventory
    {
        void AddBook(Book book);
        IEnumerable<Book> SearchForBooks(string matchcode);
        void AddBookItem(BookItem bookItem);
        IEnumerable<StockLocation> SearchForLocations(Book book);
        IEnumerable<BookItem> SearchForBookItems(Book book);
    }
}
