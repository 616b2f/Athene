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

            var article = _unitOfWork.Articles.FindArticleById(articleId.Value);

            if (article == null)
                return RedirectToAction("Index", "Inventory");

            var viewModel = new CreateInventoryItemViewModel
            {
                ArticleId = article.ArticleId,
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
                var article = _unitOfWork.Articles.FindArticleById(model.ArticleId);

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
                _unitOfWork.Inventories.AddInventoryItem(inventoryItem);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index", "Inventory");
            }

            model.Article = _unitOfWork.Articles.FindArticleById(model.ArticleId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Inventory");

            if (!(_unitOfWork.Articles.FindArticleById(id.Value) is Book book))
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
                if (!(_unitOfWork.Articles.FindArticleById(viewModel.Id) is Book book))
                {
                    this.SetUserMessage(UserMessageType.Error, _localizer["Error_BookNotFound"]);
                    return RedirectToAction("Index", "Inventory");
                }

                viewModel.ToModel(book);
                _unitOfWork.SaveChanges();
                this.SetUserMessage(UserMessageType.Success, _localizer["Success_BookCreated"]);
                return RedirectToAction("Index", "Inventory");
            }

            LoadEditViewBags();
            return View(viewModel);
        }

        private void LoadCreateViewBags()
        {
            var languages = _bookMetaProvider.AllLanguages();
            ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
            var publisher = _bookMetaProvider.AllPublisher();
            ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
            var authors = _bookMetaProvider.AllAuthors();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            var categories = _bookMetaProvider.AllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        private void LoadEditViewBags()
        {
            var languages =  _bookMetaProvider.AllLanguages();
            ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
            var publisher = _bookMetaProvider.AllPublisher();
            ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
            var authors = _bookMetaProvider.AllAuthors();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            var categories = _bookMetaProvider.AllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
