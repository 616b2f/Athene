using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.Dto;
using Athene.Inventory.Web.Mappers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
    [Area("Librarian")]
    [Route("api/[area]/[controller]")]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookMetaProvider _bookMetaProvider;
        private readonly IUnitOfWork<User> _unitOfWork;

        public AuthorsController(IUnitOfWork<User> unitOfWork,
            IBookMetaProvider bookMetaProvider)
        {
            _bookMetaProvider = bookMetaProvider;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<AuthorDto>), StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            var authors = _bookMetaProvider.AllAuthors();
            return Ok(authors.ToDto());
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreateAuthorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(CreateAuthorDto model)
        {
            if (ModelState.IsValid)
            {
                var author = model.ToEntity();
                _unitOfWork.BookMetas.AddAuthor(author);
                _unitOfWork.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest();
        }
    }
}
