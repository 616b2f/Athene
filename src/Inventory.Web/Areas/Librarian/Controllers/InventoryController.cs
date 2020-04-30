using System.Collections.Generic;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksDto;
using Athene.Inventory.Web.Dto;
using Athene.Inventory.Web.Extensions;
using Athene.Inventory.Web.Mappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ISystemClock _clock;

        public InventoryController(
            IInventoryProvider inventoryProvider,
            IArticleProvider articleProvider,
            IStringLocalizer<SharedResource> localizer,
            IUserProvider<User> userProvider,
            IUnitOfWork<User> unitOfWork,
            UserManager<User> userManager,
            ISystemClock clock)
		{
            _inventoryProvider = inventoryProvider;
            _articleProvider = articleProvider;
            _localizer = localizer;
            _userProvider = userProvider;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _clock = clock;
		}

        [HttpGet]
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
        
        [HttpPost]
        [ProducesResponseType(typeof(InventoryItem), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult AddToStore(CreateInventoryItemDto model)
        {
            if (ModelState.IsValid)
            {
                var article = _unitOfWork.Articles.FindArticleById(model.ArticleId);

                if (article == null)
                {
                    return this.NotFoundProblemDetails("not_found", "article not found");
                }

                var inventoryItem = model.ToModel();
                inventoryItem.Article = article;
                if (!string.IsNullOrWhiteSpace(model.Note))
                {
                    var note = new Abstractions.Models.ItemNote
                    { 
                        Text = model.Note,
                        CreatedAt = _clock.UtcNow.DateTime,
                        UserId = _userManager.GetUserId(User),
                    };
                    inventoryItem.Notes.Add(note);
                }
                _unitOfWork.Inventories.AddInventoryItem(inventoryItem);
                _unitOfWork.SaveChanges();
                return Ok(inventoryItem);
            }

            return this.BadRequestProblemDetails("bad_request", "model is invalid");
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<InventoryItemDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult ShowInventoryItems(int id)
        {
			if (id == 0)
				return this.BadRequestProblemDetails("bad_request", "id is not specified");

            var inventoryItems = _inventoryProvider.FindInventoryItemsByArticleId(new[] { id });
            var viewModel = inventoryItems.ToDetailsDto();
            return Ok(viewModel);
        }
    }
}