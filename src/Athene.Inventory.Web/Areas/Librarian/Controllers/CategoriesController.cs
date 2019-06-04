using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.ViewModels;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Mappers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly IBookMetaProvider _bookMetaProvider;

        public CategoriesController(IUnitOfWork<User> unitOfWork,
            IBookMetaProvider bookMetaProvider) 
        {
            _unitOfWork = unitOfWork;
            _bookMetaProvider = bookMetaProvider;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = _bookMetaProvider.AllCategories();
            return View(categories.ToViewModels());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: set message
                var category = model.ToEntity();
                var categories = new List<Category>
                {
                    category
                };
                _unitOfWork.BookMetas.AddCategories(categories);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            //TODO: set message
            return View(model);
        }
    }
}
