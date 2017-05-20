using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions
{
    public interface IBookInventory : IInventory
    {
        IEnumerable<Language> AllLanguages();
        IEnumerable<Publisher> AllPublisher();
        IEnumerable<Author> AllAuthors();
        IEnumerable<Category> AllCategories();
        IEnumerable<Book> AllBooks();
        void AddBook(Book book);
        IEnumerable<Book> SearchForBooks(string matchcode);
        IEnumerable<StockLocation> SearchForLocations(Book book);
        void AddPublisher(Publisher publisher);
        void AddAuthors(IEnumerable<Author> authors);
        void AddCategories(IEnumerable<Category> categories);
        Book FindBookById(int bookId);
    }
}
