using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Abstractions;
using Athene.Abstractions.Models;
using Athene.Inventory.Web.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy="Librarian")]
    public class AuthorsController : Controller
    {
        private readonly IInventory _inventoryService;
        private readonly IBookMetaRepository _bookMetaRepository;

        public AuthorsController(
            IInventory inventoryService,
            IBookMetaRepository bookMetaRepository) 
        {
            _inventoryService = inventoryService;
            _bookMetaRepository = bookMetaRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var authors = _bookMetaRepository.AllAuthors();
            return View(authors);
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
                var author = new Author
                {
                    FullName = model.FullName,
                    Info = model.Info,
                };
                var authors = new List<Author>();
                authors.Add(author);
                _bookMetaRepository.AddAuthors(authors);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
