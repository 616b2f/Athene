using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IArticleRepository
    {
        void AddArticle<TArticle>(TArticle article) where TArticle: Article;
        void AddArticles<TArticle>(IEnumerable<TArticle> articles) where TArticle: Article;
        IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode);
        Article FindArticleById(int articleId);
    }
}