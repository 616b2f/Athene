using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.Dto;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Mappers;
using Microsoft.AspNetCore.Http;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
    [Area("Librarian")]
    [Route("api/[area]/[controller]")]
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

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        public IActionResult Index()
        {
            var categories = _bookMetaProvider.AllCategories();
            return Ok(categories.ToDto());
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CreateCategoryDto), StatusCodes.Status201Created)]
        public IActionResult Create(CreateCategoryDto model)
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
                return Ok(model);
            }

            //TODO: set message
            return BadRequest();
        }
    }
}