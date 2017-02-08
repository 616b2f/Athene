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
    public class CategoriesController : Controller
    {
        private readonly IInventory _inventoryService;

        public CategoriesController(IInventory inventoryService) {
            _inventoryService = inventoryService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var authors = _inventoryService.AllCategories();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                //TODO: set message
                var categories = new List<Category>();
                categories.Add(model);
                _inventoryService.AddCategories(categories);
                return RedirectToAction("Index");
            }

            //TODO: set message
            return View(model);
        }
    }
}
