using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Web.Dto;
using Athene.Inventory.Web.Mappers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [ApiController]
    [Area("Librarian")]
    [Route("api/[area]/[controller]")]
    // [Authorize]
    [Authorize(Policy=Constants.Policies.Librarian)]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookMetaProvider _bookMetaProvider;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork<User> _unitOfWork;

        public AuthorsController(IUnitOfWork<User> unitOfWork,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IBookMetaProvider bookMetaProvider)
        {
            _bookMetaProvider = bookMetaProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            // if (!(await _roleManager.RoleExistsAsync(Constants.Roles.Librarian)))
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(Constants.Roles.Librarian));
            // }

            // var user = await _userManager.GetUserAsync(this.User);
            // if (user != null && !(await _userManager.IsInRoleAsync(user, Constants.Roles.Librarian)))
            // {
            //     await _userManager.AddToRoleAsync(user, Constants.Roles.Librarian);
            // }

            var authors = _bookMetaProvider.AllAuthors();
            return Ok(authors.ToDto());
        }

        [HttpPost]
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
