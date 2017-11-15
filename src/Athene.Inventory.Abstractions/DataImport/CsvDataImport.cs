using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions.DataImport
{
    public class CsvDataImport<TClass, TMapper> : IDataImport
        where TClass : class 
        where TMapper : CsvClassMap<TClass>
    {
        public string InputFormat => Constants.InputFormats.Csv;

        public string OutputFormat => typeof(TClass).Name;

        public IEnumerable<object> Convert(Stream fileStream)
        {
            var reader = new StreamReader(fileStream);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.Delimiter = ";"; //"\t";
            csvReader.Configuration.HasHeaderRecord = false;
            csvReader.Configuration.RegisterClassMap<TMapper>();
            var result = csvReader.GetRecords<TClass>().ToList(); 
            return result;
        }
    }
}