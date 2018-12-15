using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Web.Extensions;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.Areas.Librarian.Models.UsersViewModels;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Abstractions.Models;
using System.Linq;
using Microsoft.Extensions.Localization;
using Athene.Inventory.Web.Mappers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class UsersController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IInventoryRepository _inventoryService;
        private readonly IUserRepository _usersRepository;

        public UsersController(
            IStringLocalizer<SharedResource> localizer,
            IInventoryRepository inventoryService,
            IUserRepository usersRepository) 
        {
            _localizer = localizer;
            _inventoryService = inventoryService;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View();

            var users = _usersRepository.FindByMatchcode(q)
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
            if (ModelState.IsValid) {
                _usersRepository.Add(model.ToEntity());
                this.SetUserMessage(UserMessageType.Success, _localizer["Success_UserCreated"]);
                return RedirectToAction("Index");
            }
            this.SetUserMessage(UserMessageType.Error, _localizer["Error_WhileSaving"]);
            return View(model);
        }
    }
}
