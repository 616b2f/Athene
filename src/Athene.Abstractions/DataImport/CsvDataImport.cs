using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using Athene.Abstractions.Models;

namespace Athene.Abstractions.DataImport
{
    public class CsvDataImport<TClass, TMapper> : IDataImport<TClass>
        where TClass : class 
        where TMapper : CsvClassMap<TClass>
    {
        public IEnumerable<TClass> Convert(Stream fileStream)
        {
            var reader = new StreamReader(fileStream);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.Delimiter = "\t";
            csvReader.Configuration.HasHeaderRecord = false;
            csvReader.Configuration.RegisterClassMap<TMapper>();
            var result = csvReader.GetRecords<TClass>().ToList(); 
            return result;
        }
    }
}