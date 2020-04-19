using System.Collections.Generic;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.Extensions;
using Athene.Inventory.Web.Mappers;
using Athene.Inventory.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
    [Area("Librarian")]
    [Route("api/[area]/[controller]")]
    public class PublishersController : Controller
    {
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly IBookMetaProvider _bookMetaProvider;

        public PublishersController(
            IUnitOfWork<User> unitOfWork,
            IBookMetaProvider bookMetaProvider) 
        {
            _unitOfWork = unitOfWork;
            _bookMetaProvider = bookMetaProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublisherViewModel>), StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            var publisher = _bookMetaProvider.AllPublisher();
            return Ok(publisher.ToViewModels());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublisherViewModel), StatusCodes.Status200OK)]
        public IActionResult GetById(int id)
        {
            var publisher = _bookMetaProvider.FindPublisherById(id);
            return Ok(publisher.ToViewModel());
        }

        [HttpPost]
        [ProducesResponseType(typeof(PublisherViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Create(CreatePublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var publisher = model.ToEntity();
                    _unitOfWork.BookMetas.AddPublisher(publisher);
                    _unitOfWork.SaveChanges();
                    return CreatedAtAction(nameof(GetById), new { id = publisher.Id }, publisher.ToViewModel());
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return this.BadRequestProblemDetails("bad_request", "model is invalid");
        }
    }
}
