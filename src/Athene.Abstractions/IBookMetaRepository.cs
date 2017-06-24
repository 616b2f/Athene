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
        void AddAuthors(IEnumerable<Author> authors);
        void AddCategories(IEnumerable<Category> categories);
    }
}
