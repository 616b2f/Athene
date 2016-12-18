using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    public class BooksController : Controller
    {
        private readonly IInventory _inventoryService;

        public BooksController(IInventory inventoryService) {
            _inventoryService = inventoryService;
        }
        // GET: /<controller>/
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

            var books = _inventoryService.SearchForBooks(q);
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadCreateViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book model)
        {
            if (ModelState.IsValid)
            {
                _inventoryService.AddBook(model);
                // TODO: add message
                return RedirectToAction("Index");
            }

            LoadCreateViewBags();
            return View(model);
        }

        private void LoadCreateViewBags()
        {
            var languages = _inventoryService.AllLanguages();
            ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
            var publisher = _inventoryService.AllPublisher();
            ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
        }
    }
}
