using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Data.Services
{
    public class ArticleProvider : IArticleProvider
    {
        private readonly InventoryDbContext _dbContext;

        public ArticleProvider(InventoryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Article FindArticleById(int articleId)
        {
            return _dbContext.Articles
                .AsNoTracking()
                .SingleOrDefault(x => x.ArticleId == articleId);
        }

        public IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode)
        {
            return _dbContext.Articles
                .AsNoTracking()
                .Where(x => x.Matchcodes.Any(m => m.Value.Contains(matchcode)))
                .ToList();
        }
    }
}