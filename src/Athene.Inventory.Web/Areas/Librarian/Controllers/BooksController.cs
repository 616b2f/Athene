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

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
	[Area("Librarian")]
	[Authorize(Policy=Constants.Policies.Librarian)]
	public class BooksController : Controller
	{
		private readonly IInventory _inventoryService;
		private readonly IArticleRepository _articleRepository;
        private readonly IBookMetaRepository _bookMetaRepository;
        private readonly UserManager<Web.Models.ApplicationUser> _userManager;

		public BooksController(
            IInventory inventoryService, 
            IArticleRepository articleRepository,
            IBookMetaRepository bookMetaRepository,
            UserManager<Web.Models.ApplicationUser> userManager)
		{
			_inventoryService = inventoryService;
            _articleRepository = articleRepository;
            _bookMetaRepository = bookMetaRepository;
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

            var inventoryItems = _inventoryService.SearchByMatchcode(q);
            return View(inventoryItems);
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
                // TODO: add message
                return RedirectToAction("Index");
            }

            LoadCreateViewBags();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddToStore(int? bookId)
        {
            if (bookId == null)
                return RedirectToAction("Index");

            var article = _articleRepository.FindArticleById(bookId.Value);
            var book = article as Book;

            if (book == null)
                return RedirectToAction("Index");

            var viewModel = new CreateBookItemViewModel
            {
                BookId = book.Id,
                Book = book,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToStore(CreateBookItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _articleRepository.FindArticleById(model.BookId);
                var book = article as Book;

                if (book == null)
                {
                    ModelState.AddModelError(string.Empty, "Der Artikel ist kein Buch`");
                    return View(model);
                }

                var bookItem = new InventoryItem
                {
                    Article = book,
                    StockLocation = new StockLocation
                    {
                        Hall = model.Hall.Value,
                        Corridor = model.Corridor.Value,
                        Rack = model.Rack.Value,
                        Level = model.Level.Value,
                        Position = model.Position.Value,
                    }
                };
                if (!string.IsNullOrWhiteSpace(model.Note))
                {
                    //TODO: add userId
                    var note = new ItemNote
                    { 
                        Text = model.Note,
                        CreatedAt = DateTime.Now,
                        UserId = _userManager.GetUserId(User),
                    };
                    bookItem.Notes.Add(note);
                }
                _inventoryService.AddInventoryItem(bookItem);
                return RedirectToAction("Index");
            }

            return View(model);
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
    }
}
