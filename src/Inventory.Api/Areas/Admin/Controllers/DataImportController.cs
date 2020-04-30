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
        private readonly IUnitOfWork<User> _unitOfWork;

        public DataImportController(IUnitOfWork<User> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string _booksDataType = "books";
        private const string _invItemsDataType = "invItems";
        private const string _studentDataType = "students";

        private static readonly IEnumerable<IDataImport> _dataImports = new List<IDataImport> {
                new CsvDataImport<Book, BookCsvMapping>(),
                new CsvDataImport<InventoryItem, InventoryItemCsvMapping>(),
                new CsvDataImport<User, StudentCsvMapping>(),
        };

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new DataImportDto();
            PropagadaDataImportDto(viewModel);
            return View(viewModel);
        }

        private void PropagadaDataImportDto(DataImportDto viewModel)
        {
            viewModel.SourceTypes = new SelectList(new Dictionary<string,string>{
                { "CSV Datei", Athene.Inventory.Abstractions.DataImport.Constants.InputFormats.Csv },
            }, "Value", "Key");
            viewModel.DataTypes = new SelectList(new Dictionary<string, string>{
                { "Buecher", nameof(Abstractions.Models.Book) },
                { "Buch exemplare", nameof(Abstractions.Models.InventoryItem) },
                { "Schueler", nameof(Inventory.User) },
            }, "Value", "Key");
        }

        [HttpPost]
        public IActionResult Upload(DataImportDto model)
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
                case nameof(Abstractions.Models.Book):
                    var books = (IEnumerable<Book>)items;
                    serialisedData = JsonConvert.SerializeObject(books);
                    cachePrefix = _booksDataType;
                    viewName = "Books";
                    break;
                case nameof(Abstractions.Models.InventoryItem):
                    var invItems = (IEnumerable<InventoryItem>)items;
                    MapToArticles(invItems);
                    serialisedData = JsonConvert.SerializeObject(invItems, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                    cachePrefix = _invItemsDataType;
                    viewName = "InventoryItems";
                    break;
                case nameof(Inventory.User):
                    var users = (IEnumerable<User>)items;
                    serialisedData = JsonConvert.SerializeObject(users);
                    items = users.Select(u => u.ToDto());
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
                    var result = _unitOfWork.Articles.SearchForArticlesByMatchcode(book.InternationalStandardBookNumber);
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
                _unitOfWork.Articles.AddArticles(books);
                ViewBag.ImportedQuantity = books.Count();
            }
            else if (importId.StartsWith(_invItemsDataType))
            {
                var invItems = JsonConvert.DeserializeObject<IEnumerable<InventoryItem>>(serializedData, new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.All
                    });
                _unitOfWork.Inventories.AddInventoryItems(invItems);
                ViewBag.ImportedQuantity = invItems.Count();
            }
            else if (importId.StartsWith(_studentDataType))
            {
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(serializedData);
                _unitOfWork.Users.AddRange(users);
                ViewBag.ImportedQuantity = users.Count();
            }
            _unitOfWork.SaveChanges();
            return View("ImportResult");
        }
    }
}