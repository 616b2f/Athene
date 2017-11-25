using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.DataImport;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Abstractions.TestImp;
using Athene.Inventory.Web.Areas.Admin.Models;
using Athene.Inventory.Web.Mappers;
using Athene.Inventory.Web.Models;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Athene.Inventory.Web.Areas.Admin.Controllers 
{
    [Area("Admin")]
    [Authorize(Policy=Constants.Policies.DataImport)]
    [AutoValidateAntiforgeryToken]
    public class DataImportController : Controller
    {
        private readonly IUserRepository _userRepo;
        public DataImportController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        private const string _booksDataType = "books";
        private const string _invItemsDataType = "invItems";
        private const string _studentDataType = "students";

        private static IEnumerable<IDataImport> _dataImports = new List<IDataImport> {
                new CsvDataImport<Book, BookCsvMapping>(),
                new CsvDataImport<InventoryItem, InventoryItemCsvMapping>(),
                new CsvDataImport<TestUser, StudentCsvMapping>(),
        };

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new DataImportViewModel();
            PropagadaDataImportViewModel(viewModel);
            return View(viewModel);
        }

        private void PropagadaDataImportViewModel(DataImportViewModel viewModel)
        {
            viewModel.SourceTypes = new SelectList(new Dictionary<string,string>{
                { "CSV Datei", Athene.Inventory.Abstractions.DataImport.Constants.InputFormats.Csv },
            }, "Value", "Key");
            viewModel.DataTypes = new SelectList(new Dictionary<string,string>{
                { "Buecher", nameof(Book) },
                { "Buch exemplare", nameof(InventoryItem) },
                { "Schueler", nameof(TestUser) },
            }, "Value", "Key");
        }

        [HttpPost]
        public IActionResult Upload(DataImportViewModel model)
        {
            var dataImport = _dataImports.SingleOrDefault(x => 
                x.InputFormat == model.SourceType &&
                x.OutputFormat == model.DataType);
            if (dataImport == null)
                throw new NotImplementedException();
            
            var items = dataImport.Convert(model.UploadFile.OpenReadStream());
            switch (dataImport.OutputFormat)
            {
                case nameof(Book):
                    return View("Books", items);
                case nameof(InventoryItem):
                    return View("InventoryItems", items);
                case nameof(TestUser):
                    var users = (IEnumerable<IUser>)items;
                    var serialisedData = JsonConvert.SerializeObject(users);
                    var importId = Guid.NewGuid();
                    ViewBag.ImportId = importId;
                    HttpContext.Session.SetString("Students_" + importId, serialisedData);
                    items = users.Select(u => u.ToViewModel());
                    return View("Students", items);
                default:
                    throw new NotImplementedException();
            }
            // return View();
        }

        [HttpPost]
        public IActionResult Import(string importId)
        {
            var cacheImportId = "Students_" + importId;
            var serializedData = HttpContext.Session.GetString(cacheImportId);
            var users = JsonConvert.DeserializeObject<IEnumerable<ApplicationUser>>(serializedData);
            ImportAllUsers(users);
            ViewBag.ImportedQuantity = users.Count();
            return View("ImportResult");
        }

        private void ImportAllUsers(IEnumerable<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                _userRepo.Add(user);
            }
        }
    }
}