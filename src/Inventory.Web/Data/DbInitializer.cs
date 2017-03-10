using System;
using System.Collections.Generic;
using System.Security.Claims;
using Athene.Inventory.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Athene.Inventory.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(this InventoryDbContext context,
                UserManager<ApplicationUser> userManager)
        {
            var prenticeHall = new Publisher { Name = "Prentice Hall" };

            var robertCMartin = new Author { FullName = "Robert C. Martin" };
            var deanWampler = new Author { FullName = "Dean Wampler" };

            var english = new Language { Name = "English" };
            var german = new Language { Name = "Deutsch" };
            var russian = new Language { Name = "Russian" };

            var computerCategory = new Category { Name = "Computers" };

            var book1 = new Book
            {
                InternationalStandardBookNumber = "9780132350884",
                Title = "Clean Code",
                SubTitle = "A Handbook of Agile Software Craftsmanship",
                Description = "Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer–but only if you work at it.",
                Publisher = prenticeHall,
                Authors = new HashSet<Author> {
                    robertCMartin,
                    deanWampler
                },
                Categories = new HashSet<Category> {
                    computerCategory,
                },
                Language = english
            };
            var book2 = new Book
            {
                InternationalStandardBookNumber = "9780137081073",
                Title = "The Clean Coder",
                SubTitle = "A Code of Conduct for Professional Programmers",
                Description = "In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship. This book is packed with practical advice–about everything from estimating and coding to refactoring and testing. It covers much more than technique: It is about attitude. Martin shows how to approach software development with honor, self-respect, and pride; work well and work clean; communicate and estimate faithfully; face difficult decisions with clarity and honesty; and understand that deep knowledge comes with a responsibility to act.",
                Publisher = prenticeHall,
                Authors = new HashSet<Author> {
                    robertCMartin,
                },
                Categories = new HashSet<Category> {
                    computerCategory,
                },
                Language = english
            };
            context.Books.Add(book1);
            context.Books.Add(book2);

            context.Languages.Add(english);
            context.Languages.Add(german);
            context.Languages.Add(russian);

            context.Categories.Add(computerCategory);

            var bookItem1 = new BookItem
            {
                Book = book1,
                StockLocation = new StockLocation
                {
                    Hall = 1,
                    Corridor = 1,
                    Rack = 1,
                    Level = 1,
                    Position = 1,
                },
            };
            var bookItem2 = new BookItem
            {
                Book = book1,
                StockLocation = new StockLocation
                {
                    Hall = 1,
                    Corridor = 1,
                    Rack = 1,
                    Level = 1,
                    Position = 2,
                },
                RentedByUserId = null,
                RentedAt = null,
            };
            context.BookItems.Add(bookItem1);
            context.BookItems.Add(bookItem2);

            context.SaveChanges();

            var user1 = new ApplicationUser
            {
                UserName = "admin@athene.com",
                Email =  "admin@athene.com",
                Surname = "Max",
                Lastname = "Mustermann",
                Birthsday = new DateTime(1988, 1, 1),
            };
            user1.Claims.Add(new IdentityUserClaim<string>
            {
                ClaimType = ClaimTypes.Role,
                ClaimValue = "Librarian"
            });
            user1.Claims.Add(new IdentityUserClaim<string>
            {
                ClaimType = ClaimTypes.Role,
                ClaimValue = "Administrator"
            });
            var user2 = new ApplicationUser
            {
                UserName = "u.musterfrau@athene.com",
                Email =  "u.musterfrau@athene.com",
                Surname = "Uwe",
                Lastname = "Musterfrau",
                Birthsday = new DateTime(1989, 1, 1),
            };
            user2.Claims.Add(new IdentityUserClaim<string>
            {
                ClaimType = ClaimTypes.Role,
                ClaimValue = "Librarian"
            });
            var res1 = userManager.CreateAsync(user1, "Admin123!").Result;
            var res2 = userManager.CreateAsync(user2, "Test123!").Result;

            user1 = userManager.FindByEmailAsync("admin@athene.com").Result;
            bookItem1.RentedByUserId = user1.Id;
            bookItem1.RentedAt = DateTime.Now;
            context.SaveChanges();
        }
    }
}
