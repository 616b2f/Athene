using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksViewModels;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    public class BooksController : Controller
    {
        private readonly IInventory _inventoryService;

        public BooksController(IInventory inventoryService) {
            _inventoryService = inventoryService;
        }
        // GET: /<controller>/
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

            var books = _inventoryService.SearchForBooks(q);
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadCreateViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book model, int[] authorsIds, int[] categoriesIds)
        {
            if (ModelState.IsValid)
            {
                model.Authors.Clear();
                foreach(int authorId in authorsIds)
                {
                    model.Authors.Add(new Author { Id = authorId });
                }
                model.Categories.Clear();
                foreach(int categoryId in categoriesIds)
                {
                    model.Categories.Add(new Category { Id = categoryId });
                }
                _inventoryService.AddBook(model);
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

            var book = _inventoryService.FindBookById(bookId.Value);

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
                var book = _inventoryService.FindBookById(model.BookId);
                var bookItem = new BookItem
                {
                    Book = book,
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
                    var note = new BookItemNote
                    { 
                        Text = model.Note,
                        CreatedAt = DateTime.Now,
                    };
                    bookItem.Notes.Add(note);
                }
                _inventoryService.AddBookItem(bookItem);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private void LoadCreateViewBags()
        {
            var languages = _inventoryService.AllLanguages();
            ViewBag.LanguageId = new SelectList(languages, "Id", "Name");
            var publisher = _inventoryService.AllPublisher();
            ViewBag.PublisherId = new SelectList(publisher, "Id", "Name");
            var authors = _inventoryService.AllAuthors();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            var categories = _inventoryService.AllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
