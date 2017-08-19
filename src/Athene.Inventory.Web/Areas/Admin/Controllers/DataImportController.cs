using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athene.Abstractions.DataImport;
using Athene.Abstractions.Models;
using Athene.Inventory.Web.Areas.Admin.Models;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Athene.Inventory.Web.Areas.Admin.Controllers 
{
    [Area("Admin")]
    [Authorize(Policy="Librarian")]
    public class DataImportController : Controller
    {
        private const string _csvSourceType = "csv";
        private const string _booksDataType = "books";
        private const string _invItemsDataType = "invItems";

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
                { "CSV Datei", _csvSourceType },
            }, "Value", "Key");
            viewModel.DataTypes = new SelectList(new Dictionary<string,string>{
                { "Buecher", _booksDataType },
                { "Buch exemplare", _invItemsDataType },
            }, "Value", "Key");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(DataImportViewModel model)
        {
            if (model.SourceType != _csvSourceType)
                throw new NotImplementedException();
            if (model.DataType != _booksDataType && 
                model.DataType != _invItemsDataType)
                throw new NotImplementedException();
            
            if (model.SourceType == _csvSourceType && model.DataType == _booksDataType)
            {
                var csv = new CsvDataImport<Book, BookCsvMapping>();
                var items = csv.Convert(model.UploadFile.OpenReadStream());
                return View("Books", items);
            }
            if (model.SourceType == _csvSourceType && model.DataType == _invItemsDataType)
            {
                var csv = new CsvDataImport<InventoryItem, InventoryItemCsvMapping>();
                var items = csv.Convert(model.UploadFile.OpenReadStream());
                return View("InventoryItems", items);
            }
            return View();
        }
    }
}