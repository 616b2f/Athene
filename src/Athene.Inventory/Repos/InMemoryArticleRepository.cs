using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions.TestImp
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
            PropagateIdIfZero(article);
            article.GenerateMatchcodes();
            _articles.Add(article);
        }

        public void AddArticles(IEnumerable<Article> articles)
        {
            foreach (var article in articles)
            {
                AddArticle(article);
            }
        }

        private void PropagateIdIfZero(Article article)
        {
            if (article.ArticleId == 0)
                article.ArticleId = _articles.Max(x => x.ArticleId) + 1;
        }

        public Article FindArticleById(int articleId)
        {
            return _articles.SingleOrDefault(a => a.ArticleId == articleId);
        }

        public IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode)
        {
            matchcode = matchcode.ToLower().Trim(); // normalize matchcode
            return _articles.Where(a => a.Matchcodes.Any(m => m.Value.Contains(matchcode)));
        }

        public void AddArticle<TArticle>(TArticle article) where TArticle : Article
        {
            _articles.Add(article);
        }

        public void AddArticles<TArticle>(IEnumerable<TArticle> articles) where TArticle : Article
        {
            _articles.AddRange(articles);
        }
    }
}