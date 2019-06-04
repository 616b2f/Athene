using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.ViewModels;
using Athene.Inventory.Web.Mappers;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class AuthorsController : Controller
    {
        private readonly IBookMetaProvider _bookMetaProvider;
        private readonly IUnitOfWork<User> _unitOfWork;

        public AuthorsController(IUnitOfWork<User> unitOfWork,
            IBookMetaProvider bookMetaProvider)
        {
            _bookMetaProvider = bookMetaProvider;
            _unitOfWork = unitOfWork;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var authors = _bookMetaProvider.AllAuthors();
            return View(authors.ToViewModels());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = model.ToEntity();
                _unitOfWork.BookMetas.AddAuthor(author);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
