using System.Linq;
using Athene.Inventory.Abstractions.Models;
using CsvHelper.Configuration;

namespace Athene.Inventory.Abstractions.DataImport
{
    public class BookCsvMapping : CsvClassMap<Book>
    {
        public BookCsvMapping()
        {
            Map(x => x.InternationalStandardBookNumber).Index(0);
            Map(x => x.Title).Index(1);
            Map(x => x.Authors).ConvertUsing(row => row
                .GetField<string>(2)
                .Split(',')
                .Select(v => new Author{FullName = v.Trim()})
                .ToList());
            Map(x => x.Publisher).ConvertUsing(row => 
                new Publisher{ Name = row.GetField<string>(3)});
        }
    }
}