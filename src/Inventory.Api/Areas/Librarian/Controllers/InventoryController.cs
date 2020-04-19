using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Extensions;
using Athene.Inventory.Web.Mappers;
using Athene.Inventory.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
    [Area("Librarian")]
    [Route("api/[area]/[controller]")]
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
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Article>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
				return this.BadRequestProblemDetails("bad_request", "q parameter is empty");

            var articles = _articleProvider.SearchForArticlesByMatchcode(q);
            return Ok(articles);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IEnumerable<InventoryItemDetailsViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult ShowInventoryItems(int id)
        {
			if (id == 0)
				return this.BadRequestProblemDetails("bad_request", "id is not specified");

            var inventoryItems = _inventoryProvider.FindInventoryItemsByArticleId(new[] { id });
            var userIds = inventoryItems.Select(x => x.RentedByUserId);
            var users = _userProvider.FindByUserIds(userIds);
            var viewModel = inventoryItems.ToDetailsViewModels();
            return Ok(viewModel);
        }
    }
}