using System.Collections.Generic;
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
		private readonly IInventory _inventoryService;
		private readonly IArticleRepository _articleRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly UserManager<Web.Models.ApplicationUser> _userManager;

		public InventoryController(
            IInventory inventoryService, 
            IArticleRepository articleRepository,
            IStringLocalizer<SharedResource> localizer,
            UserManager<Web.Models.ApplicationUser> userManager)
		{
			_inventoryService = inventoryService;
            _articleRepository = articleRepository;
            _localizer = localizer;
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
            var viewModel = inventoryItems.ToDetailsViewModels();
            return View(viewModel);
        }
    }
}