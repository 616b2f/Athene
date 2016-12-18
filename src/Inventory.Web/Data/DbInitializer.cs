using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(this InventoryDbContext context)
        {
            var prenticeHall = new Publisher { Name = "Prentice Hall" };

            var robertCMartin = new Author { FullName = "Robert C. Martin" };
            var deanWampler = new Author { FullName = "Dean Wampler" };

            var english = new Language { Name = "English" };
            var german = new Language { Name = "Deutsch" };
            var russian = new Language { Name = "Russian" };

            context.Books.AddRange(
                new Book
                {
                    InternationalStandardBookNumber = "9780132350884",
                    Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                    Description = "Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer–but only if you work at it.",
                    Publisher = prenticeHall,
                    Authors = new HashSet<Author> {
                        robertCMartin,
                        deanWampler
                    },
                    Language = english
                },
                new Book
                {
                    InternationalStandardBookNumber = "9780137081073",
                    Title = "The Clean Coder: A Code of Conduct for Professional Programmers",
                    Description = "In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship. This book is packed with practical advice–about everything from estimating and coding to refactoring and testing. It covers much more than technique: It is about attitude. Martin shows how to approach software development with honor, self-respect, and pride; work well and work clean; communicate and estimate faithfully; face difficult decisions with clarity and honesty; and understand that deep knowledge comes with a responsibility to act.",
                    Publisher = prenticeHall,
                    Authors = new HashSet<Author> {
                        robertCMartin,
                    }, 
                    Language = english
                }
            );

            context.Languages.Add(english);
            context.Languages.Add(german);
            context.Languages.Add(russian);

            context.SaveChanges();
        }
    }
}
