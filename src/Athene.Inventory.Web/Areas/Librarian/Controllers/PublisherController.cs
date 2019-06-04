using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Mappers;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class PublisherController : Controller
    {
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly IBookMetaProvider _bookMetaProvider;

        public PublisherController(
            IUnitOfWork<User> unitOfWork,
            IBookMetaProvider bookMetaProvider) 
        {
            _unitOfWork = unitOfWork;
            _bookMetaProvider = bookMetaProvider;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var publisher = _bookMetaProvider.AllPublisher();
            return View(publisher.ToViewModels());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var publisher = model.ToEntity();
                _unitOfWork.BookMetas.AddPublisher(publisher);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
