using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Athene.Abstractions;
using Athene.EntityFramework.Data;
using Athene.Abstractions.Models;
using System;

namespace Athene.EntityFramework.Services
{
    // public class BookInventory : IBookInventory
    // {
    //     private readonly InventoryDbContext _db;
    //     public BookInventory(InventoryDbContext dbContext)
    //     {
    //         _db = dbContext;
    //     }

    //     public void AddBook(Book book)
    //     {
    //         List<int> authorIds = book.Authors
    //             .Select(a => a.Id)
    //             .Distinct()
    //             .ToList();
    //         var authors = _db.Authors
    //             .Where(a => authorIds.Contains(a.Id))
    //             .ToList();
    //         book.Authors.Clear();
    //         foreach (var author in authors) {
    //             book.Authors.Add(author);
    //         }
    //         List<int> categoryIds = book.Categories
    //             .Select(c => c.Id)
    //             .Distinct()
    //             .ToList();
    //         var categories = _db.Categories
    //             .Where(c => categoryIds.Contains(c.Id))
    //             .ToList();
    //         book.Categories.Clear();
    //         foreach (var category in categories) {
    //             book.Categories.Add(category);
    //         }
    //         _db.Books.Add(book);
    //         _db.SaveChanges();
    //     }

    //     public IEnumerable<Book> SearchForBooks(string matchcode)
    //     {
    //         var books = _db.Books
    //             .Include(b => b.Authors)
    //             .Include(b => b.Categories)
    //             .Include(b => b.InventoryItems)
    //                 .ThenInclude(ob => ob.RentedBy)
    //             .Include(b => b.InventoryItems)
    //                 .ThenInclude(ob => ob.StockLocation)
    //             .Where(b =>
    //                 b.InternationalStandardBookNumber == matchcode ||
    //                 b.Title.Contains(matchcode) ||
    //                 b.Description.Contains(matchcode) ||
    //                 b.Authors.Any(a => a.FullName.Contains(matchcode)))
    //             .ToArray();
    //         return books;
    //     }

    //     public Book FindBookById(int bookId)
    //     {
    //         var book = _db.Books
    //             .Include(b => b.Authors)
    //             .Include(b => b.Categories)
    //             .Include(b => b.InventoryItems)
    //                 .ThenInclude(ob => ob.RentedBy)
    //             .Include(b => b.InventoryItems)
    //                 .ThenInclude(ob => ob.StockLocation)
    //             .SingleOrDefault(b => b.Id == bookId);
    //         return book;
    //     }

    //     public IEnumerable<StockLocation> SearchForLocations(Book book)
    //     {
    //         var stockLocations = _db.StockLocations
    //             .Where(sl => sl.InventoryItems.Any(bi => bi.Article.Id == book.Id))
    //             .ToArray();
    //         return stockLocations;
    //     }

    //     public IEnumerable<InventoryItem> SearchForBookItems(Book book)
    //     {
    //         var bookItems = _db.InventoryItems
    //             .Where(bi => bi.Article.Id == book.Id)
    //             .ToArray();
    //         return bookItems;
    //     }

    //     public IEnumerable<Language> AllLanguages()
    //     {
    //         var languages = _db.Languages.ToArray();
    //         return languages;
    //     }

    //     public IEnumerable<Publisher> AllPublisher()
    //     {
    //         var publisher = _db.Publisher.ToArray();
    //         return publisher;
    //     }

    //     public IEnumerable<Author> AllAuthors()
    //     {
    //         var authors = _db.Authors.ToArray();
    //         return authors;
    //     }

    //     public IEnumerable<Category> AllCategories()
    //     {
    //         var categories = _db.Categories.ToArray();
    //         return categories;
    //     }

    //     public IEnumerable<Book> AllBooks()
    //     {
    //         var books = _db.Books.ToArray();
    //         return books;
    //     }

    //     public void AddPublisher(Publisher publisher)
    //     {
    //         _db.Publisher.Add(publisher);
    //         _db.SaveChangesAsync();
    //     }

    //     public void AddAuthors(IEnumerable<Author> authors)
    //     {
    //         _db.Authors.AddRange(authors);
    //         _db.SaveChanges();
    //     }

    //     public void AddCategories(IEnumerable<Category> categories)
    //     {
    //         _db.Categories.AddRange(categories);
    //         _db.SaveChanges();
    //     }

    //     public void AddInventoryItem(InventoryItem item)
    //     {
    //        var dbStockLocation = _db.StockLocations.SingleOrDefault(sl => 
    //            sl.Hall == item.StockLocation.Hall &&
    //            sl.Corridor == item.StockLocation.Corridor &&
    //            sl.Rack == item.StockLocation.Rack &&
    //            sl.Level == item.StockLocation.Level &&
    //            sl.Position == item.StockLocation.Position);
    //        if (dbStockLocation != null)
    //            item.StockLocation = dbStockLocation;
    //        _db.InventoryItems.Add(item);
    //        _db.SaveChanges();
    //     }

    //     IEnumerable<InventoryItem> SearchByMatchcode(string matchcode)
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public InventoryItem FindInventoryItemByBarcode(string barcode)
    //     {
    //         int id;
    //         if (int.TryParse(barcode.Trim(), out id))
    //             return _db.InventoryItems
    //                 .Include(bi => bi.Article)
    //                 .Include(bi => bi.StockLocation)
    //                 .SingleOrDefault(bi => bi.Id == id);
    //         else
    //             return null;
    //     }

    //     public InventoryItem FindInventoryItemById(int id)
    //     {
    //         return _db.InventoryItems
    //             .Include(bi => bi.Article)
    //             .Include(bi => bi.StockLocation)
    //             .SingleOrDefault(bi => bi.Id == id);
    //     }
    // }
}
