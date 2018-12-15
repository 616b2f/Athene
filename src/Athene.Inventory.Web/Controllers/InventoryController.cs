using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using System.Linq;
using System;
using Athene.Inventory.Web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventory;
        private readonly IArticleRepository _articleRepo;

        public InventoryController(
            IInventoryRepository inventory,
            IArticleRepository articleRepo) 
        {
            _inventory = inventory;
            _articleRepo = articleRepo;
        }

        [HttpGet]
        public IActionResult Index(string q)
        {
            ViewBag.SearchTargets = new List<string>
            {
                "Alle",
                "Bücher",
                "Zeitschriften",
                "Magazine",
            };

            if (string.IsNullOrEmpty(q))
                return View();

            var articles = _articleRepo.SearchForArticlesByMatchcode(q);
            var articleIds = articles.Select(x => x.Id).ToArray();
            var invItems = _inventory.FindInventoryItemsByArticleId(articleIds);
            Propagate(articles, invItems);
            SetDefaultImageIfEmpty(articles);
            var searchResult = new SearchResultViewModel
            {
                FoundQuantity = articles.Count(),
                Items = articles,
            };
            return View(searchResult);
        }

        private void SetDefaultImageIfEmpty(IEnumerable<Article> articles)
        {
            foreach (var article in articles)
            {
                if (string.IsNullOrWhiteSpace(article.ImageUrl))
                    article.ImageUrl = "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB3aWR0aD0iMTcxIiBoZWlnaHQ9IjE4MCIgdmlld0JveD0iMCAwIDE3MSAxODAiIHByZXNlcnZlQXNwZWN0UmF0aW89Im5vbmUiPjwhLS0KU291cmNlIFVSTDogaG9sZGVyLmpzLzEwMCV4MTgwCkNyZWF0ZWQgd2l0aCBIb2xkZXIuanMgMi42LjAuCkxlYXJuIG1vcmUgYXQgaHR0cDovL2hvbGRlcmpzLmNvbQooYykgMjAxMi0yMDE1IEl2YW4gTWFsb3BpbnNreSAtIGh0dHA6Ly9pbXNreS5jbwotLT48ZGVmcz48c3R5bGUgdHlwZT0idGV4dC9jc3MiPjwhW0NEQVRBWyNob2xkZXJfMTU5ODgxYjRlYzAgdGV4dCB7IGZpbGw6I0FBQUFBQTtmb250LXdlaWdodDpib2xkO2ZvbnQtZmFtaWx5OkFyaWFsLCBIZWx2ZXRpY2EsIE9wZW4gU2Fucywgc2Fucy1zZXJpZiwgbW9ub3NwYWNlO2ZvbnQtc2l6ZToxMHB0IH0gXV0+PC9zdHlsZT48L2RlZnM+PGcgaWQ9ImhvbGRlcl8xNTk4ODFiNGVjMCI+PHJlY3Qgd2lkdGg9IjE3MSIgaGVpZ2h0PSIxODAiIGZpbGw9IiNFRUVFRUUiLz48Zz48dGV4dCB4PSI1OS41NTQ2ODc1IiB5PSI5NC41Ij4xNzF4MTgwPC90ZXh0PjwvZz48L2c+PC9zdmc+";
            }
        }

        private void Propagate(IEnumerable<Article> articles, IEnumerable<InventoryItem> invItems)
        {
            foreach (var article in articles)
            {
                article.InventoryItems = invItems.Where(x => x.Article.Id == article.Id).ToList();
            }
        }
    }
}
