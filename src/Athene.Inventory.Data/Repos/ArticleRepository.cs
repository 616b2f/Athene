using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Data.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly InventoryDbContext _dbContext;

        public ArticleRepository(InventoryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void AddArticle<TArticle>(TArticle article) where TArticle : Article
        {
            article.GenerateMatchcodes();
            _dbContext.Articles.Add(article);
        }

        public void AddArticles<TArticle>(IEnumerable<TArticle> articles) where TArticle : Article
        {
            foreach (var article in articles)
            {
                article.GenerateMatchcodes();
            }
            _dbContext.Articles.AddRange(articles);
        }

        public Article FindArticleById(int articleId)
        {
            return _dbContext.Articles.SingleOrDefault(x => x.ArticleId == articleId);
        }

        public IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode)
        {
            return _dbContext.Articles.Where(x => x.Matchcodes.Any(m => m.Value.Contains(matchcode)));
        }
    }
}