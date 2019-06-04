using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Data.Contexts;
using Athene.Inventory.Abstractions.Models;
using System;

namespace Athene.Inventory.Data.Services
{
    public class BookMetaRepository : IBookMetaRepository
    {
        private readonly InventoryDbContext _db;

        public BookMetaRepository(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        public void AddAuthor(Author author)
        {
            _db.Authors.Add(author);
        }

        public void AddAuthors(IEnumerable<Author> authors)
        {
            _db.Authors.AddRange(authors);
        }

        public void AddCategories(IEnumerable<Category> categories)
        {
            _db.Categories.AddRange(categories);
        }

        public void AddLanguages(IEnumerable<Language> languages)
        {
            _db.Languages.AddRange(languages);
        }

        public void AddPublisher(Publisher publisher)
        {
            _db.Publisher.Add(publisher);
        }

        public Author FindAuthorById(int id)
        {
            return _db.Authors.SingleOrDefault(a => a.Id == id);
        }

        public Category FindCategoryById(int id)
        {
            return _db.Categories.SingleOrDefault(c => c.Id == id);
        }

        public Language FindLanguageById(int id)
        {
            return _db.Languages.SingleOrDefault(l => l.Id == id);
        }

        public Publisher FindPublisherById(int id)
        {
            return _db.Publisher.SingleOrDefault(p => p.Id == id);
        }
    }
}
