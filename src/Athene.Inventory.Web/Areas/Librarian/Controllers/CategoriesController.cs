using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.ViewModels;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Mappers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class CategoriesController : Controller
    {
        private readonly IInventory _inventoryService;
        private readonly IBookMetaRepository _bookMetaRepository;

        public CategoriesController(
            IInventory inventoryService,
            IBookMetaRepository bookMetaRepository) 
        {
            _inventoryService = inventoryService;
            _bookMetaRepository = bookMetaRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = _bookMetaRepository.AllCategories();
            return View(categories.ToViewModels());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: set message
                var category = model.ToEntity();
                var categories = new List<Category>();
                categories.Add(category);
                _bookMetaRepository.AddCategories(categories);
                return RedirectToAction("Index");
            }

            //TODO: set message
            return View(model);
        }
    }
}
