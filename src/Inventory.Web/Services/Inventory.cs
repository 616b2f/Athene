using System.Linq;
using System.Collections.Generic;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Data;
using Microsoft.EntityFrameworkCore;

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
            List<int> authorIds = book.Authors
                .Select(a => a.Id)
                .Distinct()
                .ToList();
            var authors = _db.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToList();
            book.Authors.Clear();
            foreach (var author in authors) {
                book.Authors.Add(author);
            }
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void AddBookItem(BookItem bookItem)
        {
            _db.BookItems.Add(bookItem);
            _db.SaveChanges();
        }

        public IEnumerable<Book> SearchForBooks(string matchcode)
        {
            var books = _db.Books
                .Include(b => b.OwnedBooks)
                    .ThenInclude(ob => ob.RentedBy)
                .Include(b => b.OwnedBooks)
                    .ThenInclude(ob => ob.StockLocation)
                .Where(b =>
                    b.InternationalStandardBookNumber == matchcode ||
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

        public IEnumerable<Language> AllLanguages()
        {
            var languages = _db.Languages.ToArray();
            return languages;
        }

        public IEnumerable<Publisher> AllPublisher()
        {
            var publisher = _db.Publisher.ToArray();
            return publisher;
        }

        public IEnumerable<Author> AllAuthors()
        {
            var authors = _db.Authors.ToArray();
            return authors;
        }

        public IEnumerable<Category> AllCategories()
        {
            var categories = _db.Categories.ToArray();
            return categories;
        }

        public IEnumerable<Book> AllBooks()
        {
            var books = _db.Books.ToArray();
            return books;
        }

        public void AddPublisher(Publisher publisher)
        {
            _db.Publisher.Add(publisher);
            _db.SaveChangesAsync();
        }

        public void AddAuthors(IEnumerable<Author> authors)
        {
            _db.Authors.AddRange(authors);
            _db.SaveChanges();
        }
    }
}
