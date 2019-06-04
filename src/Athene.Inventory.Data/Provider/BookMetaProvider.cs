using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Data.Contexts;
using Athene.Inventory.Abstractions.Models;
using System;

namespace Athene.Inventory.Data.Services
{
    public class BookMetaProvider : IBookMetaProvider
    {
        private readonly InventoryDbContext _db;

        public BookMetaProvider(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Author> AllAuthors()
        {
            return _db.Authors
                .AsNoTracking()
                .ToArray();
        }

        public IEnumerable<Category> AllCategories()
        {
            return _db.Categories
                .AsNoTracking()
                .ToArray();
        }

        public IEnumerable<Language> AllLanguages()
        {
            return _db.Languages
                .AsNoTracking()
                .ToArray();
        }

        public IEnumerable<Publisher> AllPublisher()
        {
            return _db.Publisher
                .AsNoTracking()
                .ToArray();
        }

        public Author FindAuthorById(int id)
        {
            return _db.Authors
                .AsNoTracking()
                .SingleOrDefault(a => a.Id == id);
        }

        public Category FindCategoryById(int id)
        {
            return _db.Categories
                .AsNoTracking()
                .SingleOrDefault(c => c.Id == id);
        }

        public Language FindLanguageById(int id)
        {
            return _db.Languages
                .AsNoTracking()
                .SingleOrDefault(l => l.Id == id);
        }

        public Publisher FindPublisherById(int id)
        {
            return _db.Publisher
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }
    }
}
