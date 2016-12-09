using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;

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
    }
}
