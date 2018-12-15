using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;

namespace Athene.Inventory.Data.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly InventoryDbContext _dbContext;

        public ArticleRepository(InventoryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void AddArticle(Article article)
        {
            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
        }

        public void AddArticles(IEnumerable<Article> articles)
        {
            _dbContext.Articles.AddRange(articles);
            _dbContext.SaveChanges();
        }

        public Article FindArticleById(int articleId)
        {
            return _dbContext.Articles.SingleOrDefault(x => x.Id == articleId);
        }

        public IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode)
        {
            return _dbContext.Articles.Where(x => x.Matchcodes.Contains(matchcode));
        }

        public void UpdateArticle(Article article)
        {
            _dbContext.Articles.Update(article);
            _dbContext.SaveChanges();
        }
    }
}