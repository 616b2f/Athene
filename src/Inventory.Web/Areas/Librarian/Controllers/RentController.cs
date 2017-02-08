using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.Models;
using System.Collections.Generic;
using Athene.Inventory.Web.Areas.Librarian.Models.RentViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy="Librarian")]
    public class RentController : Controller
    {
        private readonly IStudentsRepository _usersRepository;
        private readonly IRentalService _rentalService;
        private readonly IInventory _inventoryService;

        public RentController(IStudentsRepository usersRepository, 
                IRentalService rentalService, IInventory inventoryService) {
            _usersRepository = usersRepository;
            _rentalService = rentalService;
            _inventoryService = inventoryService;
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

            var user = _usersRepository.FindByUserId(userId);
            IEnumerable<BookItem> rentedBooks = _rentalService.FindRentedBooks(userId);
            var viewModel = new RentedViewModel
            {
                User = user,
                RentedBooks = rentedBooks,
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Rent(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            var user = _usersRepository.FindByUserId(userId);
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

            _rentalService.RentBook(userId, bookItemIds);
            return RedirectToAction("Rented", new { userId = userId });
        }

        [HttpGet]
        public IActionResult RentableBookItem(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
                return Content("");

            var bookItem = _inventoryService.FindBookItemByBarcode(barcode);

            //TODO: write proper error handling, with error message on client site
            if (bookItem == null)
                return Content("");

            //TODO: show error if book is allready rented by someone else

            return PartialView("_RentBookItem", bookItem);
        }
    }
}
