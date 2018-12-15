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
		private readonly IInventoryRepository _inventoryService;
		private readonly IArticleRepository _articleRepository;
		private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;

		public InventoryController(
            IInventoryRepository inventoryService, 
            IArticleRepository articleRepository,
            IStringLocalizer<SharedResource> localizer,
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager)
		{
			_inventoryService = inventoryService;
            _articleRepository = articleRepository;
            _localizer = localizer;
            _userRepository = userRepository;
            _userManager = userManager;
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

            var articles = _articleRepository.SearchForArticlesByMatchcode(q);
            return View(articles);
        }

        [HttpGet]
        public IActionResult ShowInventoryItems(int id)
        {
			if (id == 0)
				return RedirectToAction("Index");

            var inventoryItems = _inventoryService.FindInventoryItemsByArticleId(new[] { id });
            var userIds = inventoryItems.Select(x => x.RentedByUserId);
            var users = _userRepository.FindByUserIds(userIds);
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