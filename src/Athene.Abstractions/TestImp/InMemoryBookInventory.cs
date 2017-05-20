using System;
using System.Collections.Generic;
using System.Linq;

namespace Athene.Abstractions.Models
{
    public class InMemoryBookInventory : IBookInventory
    {
        private readonly List<Author> _authors = new List<Author>();
        private readonly List<Book> _books = new List<Book>();
        private readonly List<Category> _category = new List<Category>();
        private readonly List<InventoryItem> _inventoryItems = new List<InventoryItem>();
        private readonly List<Publisher> _publisher = new List<Publisher>();
        private readonly List<Language> _languages = new List<Language>();

        public void AddAuthors(IEnumerable<Author> authors)
        {
            _authors.AddRange(authors);
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void AddCategories(IEnumerable<Category> categories)
        {
            _category.AddRange(categories);
        }

        public void AddInventoryItem(InventoryItem item)
        {
            _inventoryItems.Add(item);
        }

        public void AddPublisher(Publisher publisher)
        {
            _publisher.Add(publisher);
        }

        public IEnumerable<Author> AllAuthors()
        {
            return _authors;
        }

        public IEnumerable<Book> AllBooks()
        {
            return _books;
        }

        public IEnumerable<Category> AllCategories()
        {
            return _category;
        }

        public IEnumerable<Language> AllLanguages()
        {
            return _languages;
        }

        public IEnumerable<Publisher> AllPublisher()
        {
            return _publisher;
        }

        public Book FindBookById(int bookId)
        {
            return _books.SingleOrDefault(b => b.Id == bookId);
        }

        public InventoryItem FindInventoryItemByBarcode(string barcode)
        {
            return _inventoryItems.SingleOrDefault(x => x.Barcode == barcode);
        }

        public InventoryItem FindInventoryItemById(int id)
        {
            return _inventoryItems.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<InventoryItem> FindInventoryItemById(int[] ids)
        {
            return _inventoryItems.Where(x => ids.Contains(x.Id)).AsEnumerable();
        }

        public IEnumerable<InventoryItem> SearchByMatchcode(string matchcode)
        {
            return _inventoryItems.Where(x => 
                x.Id.ToString() == matchcode ||
                x.Barcode == matchcode ||
                x.Article.Name.Contains(matchcode));
        }

        public IEnumerable<Book> SearchForBooks(string matchcode)
        {
            return _books.Where(b => 
                b.Name.Contains(matchcode) ||
                b.Title.Contains(matchcode) ||
                b.SubTitle.Contains(matchcode) ||
                b.InternationalStandardBookNumber.Contains(matchcode));
        }

        public IEnumerable<StockLocation> SearchForLocations(Book book)
        {
            return _inventoryItems
                .Where(i => i.Article.Id == book.Id)
                .Select(i => i.StockLocation)
                .Distinct();
        }
    }
}