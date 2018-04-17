using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Abstractions.Utils;

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
            PropagateMatchcodes(article);
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
            if (article.Id == 0)
                article.Id = _articles.Max(x => x.Id) + 1;
        }

        private static void PropagateMatchcodes(Article article)
        {
                // TODO: change to more dynamic approach
            if (article is Book)
            {
                var book = article as Book;
                book.Matchcodes = MatchcodeGenerator.CreateFor(book);
            }
        }

        public Article FindArticleById(int articleId)
        {
            return _articles.SingleOrDefault(a => a.Id == articleId);
        }

        public IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode)
        {
            matchcode = matchcode.ToLower().Trim(); // normalize matchcode
            return _articles.Where(a => a.Matchcodes.Any(m => m.Contains(matchcode)));
        }

        public void UpdateArticle(Article article)
        {
            PropagateMatchcodes(article);
            Article oldArticle = _articles.First(x => x.Id == article.Id);
            int index = _articles.IndexOf(oldArticle);
            _articles[index] = article;
        }
    }
}