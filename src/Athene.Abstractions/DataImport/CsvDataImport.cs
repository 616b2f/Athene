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
            string fileContent = reader.ReadToEnd();
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.Delimiter = "\t";
            csvReader.Configuration.HasHeaderRecord = false;
            csvReader.Configuration.RegisterClassMap<TMapper>();
            var test = csvReader.GetRecords<TClass>().ToList(); 
            //TODO: really dirty workaround, rewrite as soon as possible
            if (typeof(TClass) == typeof(Book))
            {
                var delimiters = new string[] { "\r\n", "\r", "\n" };
                var rows = fileContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                var books = new List<Book>();
                foreach (var row in rows)
                {
                    var col = row.Split('\t');
                    books.Add(new Book {
                        InternationalStandardBookNumber = col[0].Trim(),
                        Title = col[1].Trim(),
                        Authors = col[2]
                            .Split(',')
                            .Select(x => new Author{FullName = x.Trim()})
                            .ToList(),
                        Publisher = new Publisher{Name = col[3].Trim()},
                    });
                }
                return (IEnumerable<TClass>)books;
            }
            return test;
        }
    }
}