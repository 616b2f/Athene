using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Athene.Inventory.Web.Mappers;
using Athene.Inventory.Web.ViewModels;
using Athene.Inventory.Web.Extensions;
using Microsoft.Extensions.Localization;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
	[Area("Librarian")]
	[Authorize(Policy=Constants.Policies.Librarian)]
	public class BooksController : Controller
	{
		private readonly IInventory _inventoryService;
		private readonly IArticleRepository _articleRepository;
        private readonly IBookMetaRepository _bookMetaRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly UserManager<Web.Models.ApplicationUser> _userManager;

		public BooksController(
            IInventory inventoryService, 
            IArticleRepository articleRepository,
            IBookMetaRepository bookMetaRepository,
            IStringLocalizer<SharedResource> localizer,
            UserManager<Web.Models.ApplicationUser> userManager)
		{
			_inventoryService = inventoryService;
            _articleRepository = articleRepository;
            _bookMetaRepository = bookMetaRepository;
            _localizer = localizer;
            _userManager = userManager;
		}

        [HttpGet]
        public IActionResult Create()
        {
            LoadCreateViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book();
                foreach(int authorId in model.authorsIds)
                {
                    book.Authors.Add(new Author { Id = authorId });
                }
                book.Categories.Clear();
                foreach(int categoryId in model.categoriesIds)
                {
                    book.Categories.Add(new Category { Id = categoryId });
                }
                _articleRepository.AddArticle(book);
                this.SetUserMessage(UserMessageType.Success, _localizer["Success_BookCreated"]);
                return RedirectToAction("Index", "Inventory");
            }

            LoadCreateViewBags();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddToStore(int? articleId)
        {
            if (articleId == null)
                return RedirectToAction("Index", "Inventory");

            var article = _articleRepository.FindArticleById(articleId.Value);

            if (article == null)
                return RedirectToAction("Index", "Inventory");

            var viewModel = new CreateInventoryItemViewModel
            {
                ArticleId = article.Id,
                Article = article,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToStore(CreateInventoryItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _articleRepository.FindArticleById(model.ArticleId);

                if (article == null)
                {
                    ModelState.AddModelError(string.Empty, "Artikel not found`");
                    return View(model);
                }

                var inventoryItem = model.ToModel();
                inventoryItem.Article = article;
                if (!string.IsNullOrWhiteSpace(model.Note))
                {
                    var note = new ItemNote
                    { 
                        Text = model.Note,
                        CreatedAt = DateTime.Now,
                        UserId = _userManager.GetUserId(User),
                    };
                    inventoryItem.Notes.Add(note);
                }
                _inventoryService.AddInventoryItem(inventoryItem);
                return RedirectToAction("Index", "Inventory");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Inventory");

            var book = _articleRepository.FindArticleById(id.Value) as Book;

            if (book == null)
            {
                this.SetUserMessage(UserMessageType.Error, _localizer["Error_BookNotFound"]);
                return RedirectToAction("Index", "Inventory");
            }

            var viewModel = book.ToEditViewModel();
            LoadEditViewBags();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditBookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var book = viewModel.ToModel();
                _articleRepository.UpdateArticle(book);
                this.SetUserMessage(UserMessageType.Success, _localizer["Success_BookCreated"]);
                return RedirectToAction("Index", "Inventory");
            }

            LoadEditViewBags();
            return View(viewModel);
        }

        private void LoadCreateViewBags()
        {
            var languages = _bookMetaRepository.AllLanguages();
            ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
            var publisher = _bookMetaRepository.AllPublisher();
            ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
            var authors = _bookMetaRepository.AllAuthors();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            var categories = _bookMetaRepository.AllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        private void LoadEditViewBags()
        {
            var languages = _bookMetaRepository.AllLanguages();
            ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
            var publisher = _bookMetaRepository.AllPublisher();
            ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
            var authors = _bookMetaRepository.AllAuthors();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            var categories = _bookMetaRepository.AllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
