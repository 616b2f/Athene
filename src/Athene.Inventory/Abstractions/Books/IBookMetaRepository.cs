using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IBookMetaRepository
    {
        void AddPublisher(Publisher publisher);
        Publisher FindPublisherById(int id);
        void AddAuthor(Author author);
        void AddAuthors(IEnumerable<Author> authors);
        void AddCategories(IEnumerable<Category> categories);
        Author FindAuthorById(int id);
        Category FindCategoryById(int id);
        void AddLanguages(IEnumerable<Language> languages);
        Language FindLanguageById(int id);
    }
}
