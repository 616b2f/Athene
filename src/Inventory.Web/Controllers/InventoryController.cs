using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Athene.Abstractions;
using Athene.Abstractions.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventory _inventory;

        public InventoryController(IInventory inventory) {
            _inventory = inventory;
        }

        [HttpGet]
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

            IEnumerable<InventoryItem> items = _inventory.SearchByMatchcode(q);
            return View(items);
        }
    }
}
