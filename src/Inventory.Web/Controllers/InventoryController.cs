using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventory _inventory;

        public InventoryController(IInventory inventory) {
            _inventory = inventory;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> books = _inventory.AllBooks();
            return View(books);
        }

        [HttpGet]
        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            return View();
        }
    }
}
