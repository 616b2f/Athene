using System;
using System.Globalization;
using Athene.Inventory.Abstractions.Models;
using CsvHelper.Configuration;

namespace Athene.Inventory.Abstractions.DataImport
{
    public class StudentCsvMapping : ClassMap<User>
    {
        public StudentCsvMapping()
        {
            Map(x => x.Lastname).Index(0);
            Map(x => x.Surname).Index(1);
            Map(x => x.StudentId).Index(2);
            Map(x => x.Birthsday).ConvertUsing(row => Convert.ToDateTime(row.GetField(3), new CultureInfo("de-DE").DateTimeFormat));
            Map(x => x.Gender).ConvertUsing(row => row.GetField(4).ToLower() == "m" ? Gender.Male : Gender.Female);
            Map(x => x.Student).ConvertUsing(row => 
                new Student 
                {
                    StudentId = row.GetField(2),
                    SchoolClass = new SchoolClass(row.GetField(5), new School{ Name = row.GetField(6)}),
                }
            );
            Map(x => x.Address).ConvertUsing(row => new Address(row.GetField(7), row.GetField(8), row.GetField(9), ""));
            Map(x => x.PhoneNumber).ConvertUsing(row => row.GetField(10));

            // Map(x => x.Article).ConvertUsing(row => 
            //     new Book { InternationalStandardBookNumber = row.GetField<string>(1)?.Trim() });
            // Map(x => x.PurchasePrice).ConvertUsing(row => row.GetField<decimal>(2));
            // Map(x => x.PurchasedAt).ConvertUsing(row => row.GetField<DateTime>(3));
            // Map(x => x.Condition).ConvertUsing(row => row.GetField<Condition>(4));
        }
    }
}