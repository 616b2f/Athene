using Xunit;
using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Abstractions.DataImport;

namespace  Athene.AbstractionTests
{
    public class CsvDataImportTests
    {
        [Fact]
        public void CsvBooks_Convert_Successfull()
        {
            var fileContent = "9783507106062\tAllgemeine und Organische Chemie\tKlaus Dehnert, Manfred J�ckel\tSchroedel Schulbuchverlag\t30,95" + Environment.NewLine +
                "9783507106161\tOrganische Chemie\tKarl Risch, Hatto Seitz\tSchroedel Schulbuchverlag\t30,95";

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            var csv = new CsvDataImport<Book, BookCsvMapping>();
            var books = csv.Convert<Book>(ms);
            Assert.Equal(2, books.Count());
            var book1 = books.Single(x => x.InternationalStandardBookNumber == "9783507106062");
            Assert.Equal(2, book1.Authors.Count());
            IEnumerable<string> authors = new List<string> {"Klaus Dehnert", "Manfred J�ckel"};
            Assert.Equal(authors, book1.Authors.Select(a => a.FullName).ToList());
        }

        [Fact]
        public void CsvInventoryItems_Books_Convert_Successfull()
        {
            var fileContent = "30137\t9783507106062\t30.95\t2017-03-29\t0" + Environment.NewLine +
                "30326\t9783507106161\t20.94\t2017-03-28\t0";

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            var csv = new CsvDataImport<InventoryItem, InventoryItemCsvMapping>();
            var items = csv.Convert<InventoryItem>(ms);
            Assert.Equal(2, items.Count());
            var item1 = items.First();
            Assert.Equal(Condition.New, item1.Condition);
            Assert.Equal("30137", item1.ExternalId);
            Assert.True(item1.Article is Book);
            Assert.True((item1.Article as Book).InternationalStandardBookNumber == "9783507106062");
            Assert.Equal(30.95m, item1.PurchasePrice);
            Assert.Equal(new DateTime(2017, 3, 29), item1.PurchasedAt);

            var item2 = items.Last();
            Assert.Equal(Condition.New, item2.Condition);
            Assert.Equal("30326", item2.ExternalId);
            Assert.True(item2.Article is Book);
            Assert.True((item2.Article as Book).InternationalStandardBookNumber == "9783507106161");
            Assert.Equal(20.94m, item2.PurchasePrice);
            Assert.Equal(new DateTime(2017, 3, 28), item1.PurchasedAt);
        }
    }
}
