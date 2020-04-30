using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Athene.Inventory.Web.Mappers;
using Athene.Inventory.Web.Dto;
using Athene.Inventory.Web.Extensions;
using Microsoft.Extensions.Localization;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
	[Area("Librarian")]
    [Route("api/[area]/[controller]")]
	[Authorize(Policy=Constants.Policies.Librarian)]
	public class BooksController : ControllerBase
	{
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly IBookMetaProvider _bookMetaProvider;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly UserManager<User> _userManager;

		public BooksController(IUnitOfWork<User> unitOfWork,
            IBookMetaProvider bookMetaProvider,
            IStringLocalizer<SharedResource> localizer,
            UserManager<User> userManager)
		{
            _unitOfWork = unitOfWork;
            _bookMetaProvider = bookMetaProvider;
            _localizer = localizer;
            _userManager = userManager;
		}

        [HttpPost]
        [ProducesResponseType(typeof(CreateBookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(CreateBookDto model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book();
                foreach(int authorId in model.AuthorsIds)
                {
                    book.Authors.Add(new Author { Id = authorId });
                }
                foreach(int categoryId in model.CategoriesIds)
                {
                    book.Categories.Add(new Category { Id = categoryId });
                }
                _unitOfWork.Articles.AddArticle(book);
                _unitOfWork.SaveChanges();
                return Ok(model);
            }

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Edit(EditBookDto viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!(_unitOfWork.Articles.FindArticleById(viewModel.Id) is Book book))
                {
                    return this.NotFoundProblemDetails("not_found", "article not found");
                }

                viewModel.ToModel(book);
                _unitOfWork.SaveChanges();
                return Ok(book);
            }

            return this.BadRequestProblemDetails("bad_request", "model is invalid");
        }

    //     private void LoadCreateViewBags()
    //     {
    //         var languages = _bookMetaProvider.AllLanguages();
    //         ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
    //         var publisher = _bookMetaProvider.AllPublisher();
    //         ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
    //         var authors = _bookMetaProvider.AllAuthors();
    //         ViewBag.Authors = new SelectList(authors, "Id", "FullName");
    //         var categories = _bookMetaProvider.AllCategories();
    //         ViewBag.Categories = new SelectList(categories, "Id", "Name");
    //     }

    //     private void LoadEditViewBags()
    //     {
    //         var languages =  _bookMetaProvider.AllLanguages();
    //         ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
    //         var publisher = _bookMetaProvider.AllPublisher();
    //         ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
    //         var authors = _bookMetaProvider.AllAuthors();
    //         ViewBag.Authors = new SelectList(authors, "Id", "FullName");
    //         var categories = _bookMetaProvider.AllCategories();
    //         ViewBag.Categories = new SelectList(categories, "Id", "Name");
    //     }
    }
}
