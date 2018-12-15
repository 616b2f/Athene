using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.ViewModels;
using Athene.Inventory.Web.Mappers;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class AuthorsController : Controller
    {
        private readonly IInventoryRepository _inventoryService;
        private readonly IBookMetaRepository _bookMetaRepository;

        public AuthorsController(
            IInventoryRepository inventoryService,
            IBookMetaRepository bookMetaRepository) 
        {
            _inventoryService = inventoryService;
            _bookMetaRepository = bookMetaRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var authors = _bookMetaRepository.AllAuthors();
            return View(authors.ToViewModels());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = model.ToEntity();
                var authors = new List<Author>();
                authors.Add(author);
                _bookMetaRepository.AddAuthors(authors);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
