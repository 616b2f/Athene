using Xunit;
using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Athene.Abstractions;
using Athene.Abstractions.Models;
using Athene.Abstractions.DataImport;

namespace  Athene.AbstractionTests
{
    public class CsvDataImportTests
    {
        [Fact]
        public void CsvBooks_Convert_Successfull()
        {
            var fileContent = "9783507106062\tAllgemeine und Organische Chemie\tKlaus Dehnert, Manfred J�ckel\tSchroedel Schulbuchverlag\t30,95" + Environment.NewLine +
                "9783507106161\tOrganische Chemie\tKarl Risch, Hatto Seitz\tSchroedel Schulbuchverlag\t30,95";

            var ms = new MemoryStream(Encoding.ASCII.GetBytes(fileContent));
            var csv = new CsvDataImport<Book, BookCsvMapping>();
            var books = csv.Convert(ms);
            Console.WriteLine(Encoding.ASCII.GetString(ms.ToArray()));
            Assert.Equal(2, books.Count());
            var book1 = books.Single(x => x.InternationalStandardBookNumber == "9783507106062");
            Assert.Equal(2, book1.Authors.Count());
            Assert.True(book1.Authors.Any(a => a.FullName == "Klaus Dehnert"));
        }
    }
}