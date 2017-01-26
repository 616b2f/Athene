using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy="Librarian")]
    public class AuthorsController : Controller
    {
        private readonly IInventory _inventoryService;

        public AuthorsController(IInventory inventoryService) {
            _inventoryService = inventoryService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var authors = _inventoryService.AllAuthors();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author model)
        {
            if (ModelState.IsValid)
            {
                var authors = new List<Author>();
                authors.Add(model);
                _inventoryService.AddAuthors(authors);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
