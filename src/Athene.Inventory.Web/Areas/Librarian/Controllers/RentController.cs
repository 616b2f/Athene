using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using System.Collections.Generic;
using Athene.Inventory.Web.Areas.Librarian.Models.RentViewModels;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Models;
using System;
using Athene.Inventory.Web.Extensions;
using Microsoft.Extensions.Localization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class RentController : Controller
    {
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IUserProvider<User> _usersProvider;
        private readonly IInventoryProvider _inventoryProvider;

        public RentController(
            IUnitOfWork<User> unitOfWork,
            IStringLocalizer<SharedResource> localizer,
            IUserProvider<User> usersProvider, 
            IInventoryProvider inventoryProvider) 
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _usersProvider = usersProvider;
            _inventoryProvider = inventoryProvider;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Rented(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            var user = _usersProvider.FindByUserId(userId);
            IEnumerable<InventoryItem> rentedBooks = _inventoryProvider.FindRentedItemsByUser(userId);
            var viewModel = new RentedViewModel
            {
                User = user,
                RentedItems = rentedBooks,
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Rent(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            var user = _usersProvider.FindByUserId(userId);

            if (user == null)
            {
                this.SetUserMessage(UserMessageType.Error, _localizer["Error_UserNotFound"]);
                return RedirectToAction("Index");
            }

            var rentViewModel = new RentViewModel
            {
                UserId = user.Id,
                User = user,
            };
            return View(rentViewModel);
        }

        [HttpPost]
        public IActionResult Rent(string userId, int[] bookItemIds)
        {
            if (string.IsNullOrEmpty(userId) ||
                bookItemIds == null)
                return RedirectToAction("Index");

            var inventoryItems = _unitOfWork.Inventories.FindInventoryItemsById(bookItemIds);
            var now = DateTime.Now;
            foreach (var invItem in inventoryItems)
            {
                invItem.Rent(userId, now); // pass date, because we rent all at once (at the same time)
            }
            _unitOfWork.SaveChanges();
            return RedirectToAction("Rented", new { userId });
        }

        [HttpGet]
        public IActionResult RentableBookItem(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
                return Content("");

            var inventoryItem = _inventoryProvider.FindInventoryItemByBarcode<Book>(barcode);

            if (inventoryItem == null)
                inventoryItem = _inventoryProvider.FindInventoryItemByExternalId<Book>(barcode);
            //TODO: write proper error handling, with error message on client site
            if (inventoryItem == null)
                return Content("");

            //TODO: show error if book is allready rented by someone else

            return PartialView("_RentInventoryItem", inventoryItem);
        }
    }
}
