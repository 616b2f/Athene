using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Extensions;
using Athene.Inventory.Web.Mappers;
using Athene.Inventory.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class InventoryController : Controller
    {
        private readonly IInventoryProvider _inventoryProvider;
        private readonly IArticleProvider _articleProvider;
        private readonly IUserProvider<User> _userProvider;
        private readonly IStringLocalizer<SharedResource> _localizer;

		public InventoryController(
            IInventoryProvider inventoryProvider,
            IArticleProvider articleProvider,
            IStringLocalizer<SharedResource> localizer,
            IUserProvider<User> userProvider)
		{
            _inventoryProvider = inventoryProvider;
            _articleProvider = articleProvider;
            _localizer = localizer;
            _userProvider = userProvider;
		}

		[HttpGet]
        public IActionResult Index(string q)
        {
            ViewBag.SearchTargets = new List<string>
            {
                "Alle",
                "Bücher",
                "Zeitschriften",
                "Magazine",
            };

			if (string.IsNullOrEmpty(q))
				return View();

            var articles = _articleProvider.SearchForArticlesByMatchcode(q);
            return View(articles);
        }

        [HttpGet]
        public IActionResult ShowInventoryItems(int id)
        {
			if (id == 0)
				return RedirectToAction("Index");

            var inventoryItems = _inventoryProvider.FindInventoryItemsByArticleId(new[] { id });
            var userIds = inventoryItems.Select(x => x.RentedByUserId);
            var users = _userProvider.FindByUserIds(userIds);
            PropagateInventoryItemsWithUsers(inventoryItems, users);
            var viewModel = inventoryItems.ToDetailsViewModels();
            return View(viewModel);
        }

        private void PropagateInventoryItemsWithUsers(IEnumerable<InventoryItem> inventoryItems, IEnumerable<IUser> users)
        {
            foreach (var item in inventoryItems)
            {
                if (!string.IsNullOrWhiteSpace(item.RentedByUserId))
                {
                    item.RentedBy = users.SingleOrDefault(x => x.Id == item.RentedByUserId);
                }
            }
        }
    }
}