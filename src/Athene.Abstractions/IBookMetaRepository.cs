using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions
{
    public interface IBookMetaRepository
    {
        IEnumerable<Language> AllLanguages();
        IEnumerable<Publisher> AllPublisher();
        IEnumerable<Author> AllAuthors();
        IEnumerable<Category> AllCategories();
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
