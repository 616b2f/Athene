using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Athene.Inventory.Web.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    public class StudentsController : Controller
    {
        private readonly IInventory _inventoryService;
        private readonly IStudentsRepository _studentsRepository;

        public StudentsController(IInventory inventoryService, 
                IStudentsRepository studentsRepository) {
            _inventoryService = inventoryService;
            _studentsRepository = studentsRepository;
        }

        public IActionResult Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View();

            var students = _studentsRepository.Find(q);
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid) {
                _studentsRepository.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
    }
}
