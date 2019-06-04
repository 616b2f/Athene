using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IArticleProvider
    {
        Article FindArticleById(int articleId);
        IEnumerable<Article> SearchForArticlesByMatchcode(string matchcode);
    }
}