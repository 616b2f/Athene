using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Data;

namespace Athene.Inventory.Web.Services
{
    public class Inventory : IInventory
    {
        private readonly InventoryDbContext _db;
        public Inventory(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        public void AddBook(Book book)
        {
            _db.Books.Add(book);
        }

        public void AddBookItem(BookItem bookItem)
        {
            _db.BookItems.Add(bookItem);
        }

        public IEnumerable<Book> SearchForBooks(string matchcode)
        {
            var books = _db.Books
                .Where(b =>
                    b.InternationalStandardBookNumber10 == matchcode ||
                    b.InternationalStandardBookNumber13 == matchcode ||
                    b.EuropeanArticleNumber == matchcode ||
                    b.Title.Contains(matchcode) ||
                    b.Description.Contains(matchcode) ||
                    b.Authors.Any(a => a.FullName.Contains(matchcode)))
                .ToArray();

            return books;
        }

        public IEnumerable<StockLocation> SearchForLocations(Book book)
        {
            var stockLocations = _db.StockLocations
                .Where(sl => sl.BookItems.Any(bi => bi.Book.Id == book.Id))
                .ToArray();

            return stockLocations;
        }

        public IEnumerable<BookItem> SearchForBookItems(Book book)
        {
            var bookItems = _db.BookItems
                .Where(bi => bi.Book.Id == book.Id)
                .ToArray();

            return bookItems;
        }
    }
}
