using System;
using System.Globalization;
using System.Linq;
using Athene.Inventory.Abstractions.Models;
using CsvHelper.Configuration;

namespace Athene.Inventory.Abstractions.DataImport
{
    public class InventoryItemCsvMapping : CsvClassMap<InventoryItem>
    {
        public InventoryItemCsvMapping()
        {
            Map(x => x.ExternalId).Index(0);
            Map(x => x.Article).ConvertUsing(row => 
                new Book { InternationalStandardBookNumber = row.GetField<string>(1)?.Trim() });
            Map(x => x.PurchasePrice).ConvertUsing(row => row.GetField<decimal>(2));
            Map(x => x.PurchasedAt).ConvertUsing(row => row.GetField<DateTime>(3));
            Map(x => x.Condition).ConvertUsing(row => row.GetField<Condition>(4));
        }
    }
}