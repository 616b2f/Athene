using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Athene.Inventory.Abstractions.DataImport
{
    public class CsvDataImport<TClass, TMapper> : IDataImport
        where TClass : class 
        where TMapper : ClassMap<TClass>
    {
        public string InputFormat => Constants.InputFormats.Csv;

        public string OutputFormat => typeof(TClass).Name;

        public IEnumerable<TClass> Convert<TClass>(Stream fileStream, string delimiter = ";")
					where TClass : class
        {
            var reader = new StreamReader(fileStream);
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Configuration.Delimiter = delimiter; //"\t";
            csvReader.Configuration.HasHeaderRecord = false;
            csvReader.Configuration.RegisterClassMap<TMapper>();
            var result = csvReader.GetRecords<TClass>().ToList(); 
            return result;
        }
    }
}
