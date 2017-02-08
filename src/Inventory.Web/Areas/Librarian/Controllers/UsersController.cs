using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy="Librarian")]
    public class UsersController : Controller
    {
        private readonly IInventory _inventoryService;
        private readonly IStudentsRepository _usersRepository;

        public UsersController(IInventory inventoryService, 
                IStudentsRepository usersRepository) {
            _inventoryService = inventoryService;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View();

            var users = _usersRepository.Find(q);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationUser user)
        {
            //TODO: use a ViewModel instead of ApplicationUser
            if (ModelState.IsValid) {
                _usersRepository.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
