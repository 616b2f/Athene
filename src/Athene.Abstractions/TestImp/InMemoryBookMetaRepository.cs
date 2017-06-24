using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Abstractions.Models;

namespace Athene.Abstractions.TestImp
{
    public class InMemoryBookMetaRepository : IBookMetaRepository
    {
        private readonly List<Author> _authors = new List<Author>();
        private readonly List<Category> _category = new List<Category>();
        private readonly List<Publisher> _publisher = new List<Publisher>();
        private readonly List<Language> _languages = new List<Language>();

        public void AddAuthors(IEnumerable<Author> authors)
        {
            _authors.AddRange(authors);
        }

        public void AddCategories(IEnumerable<Category> categories)
        {
            _category.AddRange(categories);
        }

        public void AddPublisher(Publisher publisher)
        {
            _publisher.Add(publisher);
        }

        public IEnumerable<Author> AllAuthors()
        {
            return _authors;
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
    }
}