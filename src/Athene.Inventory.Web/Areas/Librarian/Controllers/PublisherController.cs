using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class PublisherController : Controller
    {
        private readonly IInventory _inventoryService;
        private readonly IBookMetaRepository _bookMetaRepository;

        public PublisherController(
            IInventory inventoryService,
            IBookMetaRepository bookMetaRepository) 
        {
            _inventoryService = inventoryService;
            _bookMetaRepository = bookMetaRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var publisher = _bookMetaRepository.AllPublisher();
            return View(publisher);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var publisher = new Publisher
                {
                    Name = model.Name,
                };
                _bookMetaRepository.AddPublisher(publisher);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
