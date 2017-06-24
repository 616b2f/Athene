using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions
{
    public interface IArticleRepository
    {
        void AddArticle(Article book);
        IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode);
        Article FindArticleById(int articleId);
        // IEnumerable<T> SearchArticleByMatchcode(string matchcode) where T : class;
    }
}