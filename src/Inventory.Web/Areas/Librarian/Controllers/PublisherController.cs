using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    public class PublisherController : Controller
    {
        private readonly IInventory _inventoryService;

        public PublisherController(IInventory inventoryService) {
            _inventoryService = inventoryService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var publisher = _inventoryService.AllPublisher();
            return View(publisher);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher model)
        {
            if (ModelState.IsValid)
            {
                //TODO: set message
                _inventoryService.AddPublisher(model);
                return RedirectToAction("Index");
            }

            //TODO: set message
            return View(model);
        }
    }
}
