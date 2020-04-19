using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Athene.Inventory.Web.Areas.Librarian.Models.RentViewModels;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using System;
using Athene.Inventory.Web.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
    [Area("Librarian")]
    [Route("api/[area]/[controller]")]
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

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(RentedViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Rented(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return this.BadRequestProblemDetails("bad_request", "userid not provided");

            var user = _usersProvider.FindByUserId(userId);
            if (user == null)
                return this.NotFoundProblemDetails("not_found", "user not found");

            IEnumerable<InventoryItem> rentedBooks = _inventoryProvider.FindRentedItemsByUser(userId);
            var viewModel = new RentedViewModel
            {
                User = user,
                RentedItems = rentedBooks,
            };
            return Ok(viewModel);
        }

        [HttpPost("{userId}")]
        [ProducesResponseType(typeof(IEnumerable<InventoryItem>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Rent(string userId, int[] bookItemIds)
        {
            if (string.IsNullOrEmpty(userId))
                return this.BadRequestProblemDetails("bad_request", nameof(userId) + " is not set");

            if (bookItemIds == null)
                return this.BadRequestProblemDetails("bad_request", nameof(bookItemIds) + " is not set");

            var inventoryItems = _unitOfWork.Inventories.FindInventoryItemsById(bookItemIds);
            var now = DateTime.Now;
            foreach (var invItem in inventoryItems)
            {
                invItem.Rent(userId, now); // pass date, because we rent all at once (at the same time)
            }

            try
            {
                _unitOfWork.SaveChanges();
                return CreatedAtAction(nameof(Rented), new { userId = userId }, inventoryItems);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("barcode/{barcode}")]
        [ProducesResponseType(typeof(InventoryItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult RentableBookItem(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
                return this.BadRequestProblemDetails("bad_request", "barcode not provided");

            var inventoryItem = _inventoryProvider.FindInventoryItemByBarcode<Book>(barcode);

            if (inventoryItem == null)
                inventoryItem = _inventoryProvider.FindInventoryItemByExternalId<Book>(barcode);

            //TODO: write proper error handling, with error message on client site
            if (inventoryItem == null)
                return this.NotFoundProblemDetails("not_found", "book for barcode not found");

            //TODO: show error if book is allready rented by someone else

            return Ok(inventoryItem);
        }
    }
}
