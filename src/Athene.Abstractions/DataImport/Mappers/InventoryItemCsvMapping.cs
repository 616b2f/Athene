
using System.Linq;
using Athene.Abstractions.Models;
using CsvHelper.Configuration;

namespace Athene.Abstractions.DataImport
{
    public class InventoryItemCsvMapping : CsvClassMap<InventoryItem>
    {
        public InventoryItemCsvMapping()
        {
            Map(x => x.Id).Index(0);
            Map(x => x.Article).ConvertUsing(row => 
                new Book { InternationalStandardBookNumber = row.GetField<string>(1) });
            Map(x => x.PurchasePrice).Index(2);
            Map(x => x.Condition).ConvertUsing(row => row.GetField<Condition>(3) );
        }
    }
}