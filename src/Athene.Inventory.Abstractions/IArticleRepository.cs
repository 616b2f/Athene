using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IArticleRepository
    {
        void AddArticle(Article article);
        void AddArticles(IEnumerable<Article> articles);
        void UpdateArticle(Article article);
        IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode);
        Article FindArticleById(int articleId);
    }
}