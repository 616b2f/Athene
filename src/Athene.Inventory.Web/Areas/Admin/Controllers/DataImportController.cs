using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.DataImport;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Admin.Models;
using Athene.Inventory.Web.Mappers;
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
        private readonly IInventoryRepository _inventory;
        private readonly IArticleRepository _articleRepo;

        public DataImportController(
            IUserRepository userRepo,
            IInventoryRepository inventory,
            IArticleRepository articleRepo)
        {
            _userRepo = userRepo;
            _inventory = inventory;
            _articleRepo = articleRepo;
        }

        private const string _booksDataType = "books";
        private const string _invItemsDataType = "invItems";
        private const string _studentDataType = "students";

        private static IEnumerable<IDataImport> _dataImports = new List<IDataImport> {
                new CsvDataImport<Book, BookCsvMapping>(),
                new CsvDataImport<InventoryItem, InventoryItemCsvMapping>(),
                new CsvDataImport<ApplicationUser, StudentCsvMapping>(),
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
                { "Schueler", nameof(ApplicationUser) },
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
            string serialisedData = "";
            string cachePrefix = "";
            string viewName = "";
            switch (dataImport.OutputFormat)
            {
                case nameof(Book):
                    var books = (IEnumerable<Book>)items;
                    serialisedData = JsonConvert.SerializeObject(books);
                    cachePrefix = _booksDataType;
                    viewName = "Books";
                    break;
                case nameof(InventoryItem):
                    var invItems = (IEnumerable<InventoryItem>)items;
                    MapToArticles(invItems);
                    serialisedData = JsonConvert.SerializeObject(invItems, new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.All
                    });
                    cachePrefix = _invItemsDataType;
                    viewName = "InventoryItems";
                    break;
                case nameof(ApplicationUser):
                    var users = (IEnumerable<IUser>)items;
                    serialisedData = JsonConvert.SerializeObject(users);
                    items = users.Select(u => u.ToViewModel());
                    cachePrefix = _studentDataType;
                    viewName = "Students";
                    break;
                default:
                    throw new NotImplementedException();
            }
            string importId = $"{cachePrefix}_{Guid.NewGuid()}";
            ViewBag.ImportId = importId;
            HttpContext.Session.SetString(importId, serialisedData);
            return View(viewName, items);
        }

        private void MapToArticles(IEnumerable<InventoryItem> invItems)
        {
            foreach (var invItem in invItems)
            {
                if (invItem.Article is Book)
                {
                    var book = invItem.Article as Book;
                    var result = _articleRepo.SearchForArticlesByMatchcode(book.InternationalStandardBookNumber);
                    // TODO: if found more then 1 add error for imported inventoryItem and if Article is null too
                    if (result.Count() <= 1)
                        invItem.Article = (Book)result.FirstOrDefault();
                    else
                        invItem.Article = null;
                }
            }
        }

        [HttpPost]
        public IActionResult Import(string importId)
        {
            string serializedData = HttpContext.Session.GetString(importId);
            if (importId.StartsWith(_booksDataType))
            {
                var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(serializedData);
                _articleRepo.AddArticles(books);
                ViewBag.ImportedQuantity = books.Count();
            }
            else if (importId.StartsWith(_invItemsDataType))
            {
                var invItems = JsonConvert.DeserializeObject<IEnumerable<InventoryItem>>(serializedData, new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.All
                    });
                _inventory.AddInventoryItems(invItems);
                ViewBag.ImportedQuantity = invItems.Count();
            }
            else if (importId.StartsWith(_studentDataType))
            {
                var users = JsonConvert.DeserializeObject<IEnumerable<ApplicationUser>>(serializedData);
                ImportAllUsers(users);
                ViewBag.ImportedQuantity = users.Count();
            }
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