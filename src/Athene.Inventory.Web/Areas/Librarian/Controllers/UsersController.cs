using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Web.Extensions;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.Areas.Librarian.Models.UsersViewModels;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Abstractions.Models;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy="Librarian")]
    public class UsersController : Controller
    {
        private readonly IInventory _inventoryService;
        private readonly IUserRepository _usersRepository;

        public UsersController(
            IInventory inventoryService,
            IUserRepository usersRepository) 
        {
            _inventoryService = inventoryService;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View();

            var users = _usersRepository.Find(q)
                .Cast<ApplicationUser>();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel model)
        {
            //TODO: use a ViewModel instead of ApplicationUser
            if (ModelState.IsValid) {
                var appUser = new ApplicationUser
                {
                    Surname = model.Surname,
                    Lastname = model.Lastname,
                    Gender = model.Gender,
                    Address = new Address(model.AddressStreet, model.AddressZip, model.AddressCity, model.AddressCountry),
                    Birthsday = model.Birthsday,
                    StudentId = model.StudentId,
                };
                _usersRepository.Add(appUser);
                this.SetUserMessage(UserMessageType.Success, "Benutzer erfolgreich erstellt");
                return RedirectToAction("Index");
            }
            this.SetUserMessage(UserMessageType.Error, "Fehler beim Speichern aufgetreten.");
            return View(model);
        }
    }
}
