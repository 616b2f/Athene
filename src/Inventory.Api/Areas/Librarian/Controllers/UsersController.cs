using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Web.Extensions;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.Areas.Librarian.Models.UsersDto;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Abstractions.Models;
using System.Linq;
using Microsoft.Extensions.Localization;
using Athene.Inventory.Web.Mappers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
    [Area("Librarian")]
    [Route("api/[area]/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IUserProvider<User> _usersProvider;

        public UsersController(
            IUnitOfWork<User> unitOfWork,
            IStringLocalizer<SharedResource> localizer,
            IUserProvider<User> usersProvider) 
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _usersProvider = usersProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return this.BadRequestProblemDetails("bad_request", nameof(q) + " parameter is empty");

            var users = _usersProvider.FindByMatchcode(q);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.BadRequestProblemDetails("bad_request", nameof(id) + " is not provided");

            var user = _usersProvider.FindByUserId(id);
            if (user != null)
            {
                return Ok(user);
            }
            
            return this.NotFoundProblemDetails("not_found", "user not found");
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Create(CreateUserDto model)
        {
            if (ModelState.IsValid) {
                var entity = model.ToEntity();
                try
                {
                    _unitOfWork.Users.Add(entity);
                    _unitOfWork.SaveChanges();
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
            }
            return this.BadRequestProblemDetails("bad_request", "model is invalid");
        }
    }
}
