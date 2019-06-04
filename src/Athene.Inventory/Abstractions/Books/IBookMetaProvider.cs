using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IBookMetaProvider
    {
        IEnumerable<Author> AllAuthors();
        IEnumerable<Category> AllCategories();
        IEnumerable<Language> AllLanguages();
        IEnumerable<Publisher> AllPublisher();
        Author FindAuthorById(int id);
        Category FindCategoryById(int id);
        Language FindLanguageById(int id);
        Publisher FindPublisherById(int id);
    }
}