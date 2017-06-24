using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Abstractions.Models;

namespace Athene.Abstractions.TestImp
{
    public class InMemoryArticleRepository : IArticleRepository
    {
        private readonly List<Article> _articles;
        public InMemoryArticleRepository()
        {
            _articles = new List<Article>();
        }

        public void AddArticle(Article article)
        {
            _articles.Add(article);
        }

        public Article FindArticleById(int articleId)
        {
            return _articles.SingleOrDefault(a => a.Id == articleId);
        }

        public IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode)
        {
            return _articles.Where(a => a.Matchcodes.Any(m => m.Contains(matchcode)));
        }
    }
}