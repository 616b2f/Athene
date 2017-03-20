using System;
using System.Collections.Generic;
using System.Security.Claims;
using Athene.Abstractions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Athene.EntityFramework.Data
{
    // public static class DbInitializer
    // {
    //     public static void Initialize(this InventoryDbContext context,
    //             UserManager<ApplicationUser> userManager)
    //     {
    //         /* Publishers */
    //         var prenticeHall = new Publisher { Name = "Prentice Hall" };
    //         var pearson = new Publisher { Name = "Pearson" };
    //         var thomsonReuters = new Publisher { Name = "Thomson Reuters" };
    //         var relxGroup = new Publisher { Name = "RELX Group" };
    //         var PenguinRandomHouse = new Publisher { Name = "Penguin Random House" };
    //         var woltersKluwer = new Publisher { Name = "Wolters Kluwer" };
    //         var rheinwerk = new Publisher { Name = "Rheinwerk" };
    //         var hanserVerlag = new Publisher { Name = "Der Carl Hanser Verlag" };
    //         var cornelsen = new Publisher { Name = "Cornelsen Verlag GmbH" };
    //         var ernstKlett = new Publisher { Name = "Ernst Klett Verlag GmbH" };

    //         /* Authors */
    //         var robertCMartin = new Author { FullName = "Robert C. Martin" };
    //         var deanWampler = new Author { FullName = "Dean Wampler" };
    //         var benAaronovitch = new Author { FullName = "Ben Aaronovitch" };
    //         var atiaAbawi = new Author { FullName = "Atia Abawi" };
    //         var davidAbbott = new Author { FullName = "David Abbott" };
    //         var emmyAbrahamson = new Author { FullName = "Emmy Abrahamson" };
    //         var sashaAbramsky = new Author { FullName = "Sasha Abramsky" };
    //         var thomasClaudiusHuber = new Author { FullName = "Thomas Claudius Huber" };
    //         var andrewTannenbaum = new Author { FullName = "Andrew S. Tannenbaum" };
    //         var herberBos = new Author { FullName = "Herbert Bos" };
    //         var rolfSocher = new Author { FullName = "Rolf Socher" };
    //         var susanAbbey = new Author { FullName = "Susan Abbey" };
    //         var roderickCox = new Author { FullName = "Roderick Cox" };
    //         var laurenceHarger = new Author { FullName = "Laurnce Harger" };
    //         var claireLamsdale = new Author { FullName = "Claire Lemsdale" };
    //         var wolfgangbiederstaedt = new Author { FullName = "Wolfgang Biederstädt" };
    //         var hellmutSchwarz = new Author { FullName = "Hellmut Schwarz" };
    //         var antonioBarquero = new Author { FullName = "Antonio Barquero" };
    //         var julianaBizama = new Author { FullName = "Juliana Bizama" };
    //         var jaimeCorpas = new Author { FullName = "Jaime Corpas" };
    //         var pedroCrovetto = new Author { FullName = "Pedro Crovetto-Bizama" };
    //         var evaDiaz = new Author { FullName = "Eva Diaz" };
    //         var claireMarieJeske = new Author { FullName = "Claire-Marie Jeske" };
    //         var aliciaJimenez = new Author { FullName = "Alicia Jimenez Romera" };
    //         var javierNavarro = new Author { FullName = "Javier Navarro" };
    //         var rosamnaPardellas = new Author { FullName = "Rosamna Pradellas Velay" };

    //         /* Languaged */
    //         var english = new Language { Name = "English" };
    //         var german = new Language { Name = "Deutsch" };
    //         var russian = new Language { Name = "Russian" };
    //         var spain = new Language { Name = "Spanisch" };
    //         var italian = new Language { Name = "Italienisch" };

    //         /* Categories */
    //         var computerCategory = new Category { Name = "Computers" };
    //         var thrillerCategory = new Category { Name = "Thrillers" };
    //         var specializedCategory = new Category { Name = "Fachbuch" };
    //         var literatureCategory = new Category { Name = "Literatur" };
    //         var humorCategory = new Category { Name = "Humor" };
    //         var codingCategory = new Category { Name = "Programmieren" };
    //         var schoolCategory = new Category { Name = "Schulbücher" };

    //         /* Board of education */
    //         var boardOfEducation = new BoardOfEducation("Landrat");

    //         /* School addresses */
    //         var schhol1Address = new Address("Musterstraße 1", "88451", "Dettingen a. d. Iller", "DE");
    //         var schhol2Address = new Address("Musterstraße 2", "88451", "Dettingen a. d. Iller", "DE");

    //         /* Schools */
    //         var school1 = new School("Schule 1", "SC1", schhol1Address, boardOfEducation);
    //         var school2 = new School("Schule 2", "SC2", schhol2Address, boardOfEducation);

    //         /* school classes */
    //         var schoolClass1 = new SchoolClass("BF1", school1);
    //         var schoolClass2 = new SchoolClass("BF2", school1);
    //         var schoolClass3 = new SchoolClass("BR2", school2);
    //         var schoolClass4 = new SchoolClass("BE2", school2);
    //         school1.SchoolClasses.Add(schoolClass1);
    //         school1.SchoolClasses.Add(schoolClass2);
    //         school2.SchoolClasses.Add(schoolClass3);
    //         school2.SchoolClasses.Add(schoolClass4);

    //         /* Books */
    //         var book1 = new Book
    //         {
    //             InternationalStandardBookNumber = "9780132350884",
    //             Title = "Clean Code",
    //             SubTitle = "A Handbook of Agile Software Craftsmanship",
    //             Description = "Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer–but only if you work at it.",
    //             Publisher = prenticeHall,
    //             Authors = new HashSet<Author> {
    //                 robertCMartin,
    //                 deanWampler
    //             },
    //             Categories = new HashSet<Category> {
    //                 computerCategory,
    //             },
    //             Language = english
    //         };
    //         var book2 = new Book
    //         {
    //             InternationalStandardBookNumber = "9780137081073",
    //             Title = "The Clean Coder",
    //             SubTitle = "A Code of Conduct for Professional Programmers",
    //             Description = "In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship. This book is packed with practical advice–about everything from estimating and coding to refactoring and testing. It covers much more than technique: It is about attitude. Martin shows how to approach software development with honor, self-respect, and pride; work well and work clean; communicate and estimate faithfully; face difficult decisions with clarity and honesty; and understand that deep knowledge comes with a responsibility to act.",
    //             Publisher = prenticeHall,
    //             Authors = new HashSet<Author> {
    //                 robertCMartin,
    //             },
    //             Categories = new HashSet<Category> {
    //                 computerCategory,
    //                 literatureCategory
    //             },
    //             Language = english
    //         };
    //         var book3 = new Book
    //         {
    //             InternationalStandardBookNumber = "9783836219563",
    //             Title = "Windows Presentation Forms",
    //             SubTitle = "WPF - Das umfassende Handbuch",
    //             Description = "Geballtes Wissen zum Grafik-Framework von .NET! Ob Grundlagen, XAML, GUI-Entwicklung, Datenbindung, Animationen, Multimedia oder Migration - hier finden Sie auf jede Frage eine Antwort! Grundkenntnisse in C# vorausgesetzt, ist dieses Buch sowohl zum Einstieg als auch als Nachschlagewerk optimal geeignet.",
    //             Publisher = rheinwerk,
    //             Authors = new HashSet<Author> {
    //                 thomasClaudiusHuber,
    //             },
    //             Categories = new HashSet<Category> {
    //                 computerCategory,
    //                 codingCategory,
    //                 literatureCategory
    //             },
    //             Language = german
    //         };
    //         var book4 = new Book
    //         {
    //             InternationalStandardBookNumber = "9783836219563",
    //             Title = "Moderne Betriebssysteme",
    //             SubTitle = "Moderne Betriebssysteme (4., aktualisierte Auflage)",
    //             Description = "Auch in dieser komplett überarbeiteten Neuauflage des preisgekrönten Lehrbuchs stellen die Autoren wie gewohnt auf unterhaltsame Art und Weise alle Konzepte rund um das Thema Betriebssysteme vor, die man benötigt, um moderne Betriebssysteme zu verstehen und zu entwickeln. Dabei wurden den neuesten Entwicklungen der Betriebssysteme sowie der zugrunde liegenden Hardware Rechnung getragen. Das Lehrbuch enthält umfangreiche Aktualisierungen zu UNIX, Linux und Windows und behandelt erstmals auch Android als mobiles Betriebssystem. Das Kapitel zu IT-Sicherheit wurde grundlegend aktualisiert, ein neues Kapitel behandelt die Themen Virtualisierungs- und Cloud-Technologie.  Zahlreiche Abbildungen, viele Beispiele, aktuelle Fallstudien sowie über 500 Übungsaufgaben erleichtern das Verstehen und Erlernen der vorgestellten Konzepte und Theorien. Zudem stehen auf den Companion Websites zum Buch Experimentier- und Simulationswerkzeuge zu Linux und Window bereit – ideal zum Selbststudium.",
    //             Publisher = rheinwerk,
    //             Authors = new HashSet<Author> {
    //                 andrewTannenbaum,
    //                 herberBos
    //             },
    //             Categories = new HashSet<Category> {
    //                 computerCategory,
    //                 literatureCategory
    //             },
    //             Language = german
    //         };
    //         var book5 = new Book
    //         {
    //             InternationalStandardBookNumber = "9783446414457",
    //             Title = "Theoretische Grundlagen der Informatik",
    //             SubTitle = "",
    //             Description = "Das Buch bietet einen Einstieg in die theoretischen Grundlagen der Informatik. Es beschränkt sich auf die klassischen Themen: formale Sprachen, endliche Automaten und Grammatiken, Turing-Maschinen, Berechenbarkeit und Entscheidbarkeit, Komplexität. Das Konzept der Transformation zwischen den verschiedenen Formalismen zieht sich wie ein roter Faden durch das gesamte Buch. Auf eine anschauliche Vermittlung der Begriffe und Methoden der theoretischen Informatik und ihre Vertiefung in Aufgaben und Programmierprojekten wird großer Wert gelegt. Auf der zu dem Buch gehörenden Website findet sich das Lernprogramm Machines, mit dem endliche Automaten, Kellerautomaten, Grammatiken, reguläre Ausdrücke und Turing-Maschinen mit einer komfortablen grafischen Oberfläche realisiert und visualisiert werden können.",
    //             Publisher = hanserVerlag,
    //             Authors = new HashSet<Author> {
    //                 rolfSocher
    //             },
    //             Categories = new HashSet<Category> {
    //                 computerCategory,
    //                 literatureCategory
    //             },
    //             Language = german
    //         };
    //         var book6 = new Book
    //         {
    //             InternationalStandardBookNumber = "9783060313150",
    //             Title = "English G 21",
    //             SubTitle = "",
    //             Description = "Eine systematische Prüfungsvorbereitung mit drei kürzeren Units, die dem Aufbau von Band 5 folgen",
    //             Publisher = cornelsen,
    //             Authors = new HashSet<Author> {
    //                 susanAbbey,
    //                 roderickCox,
    //                 laurenceHarger,
    //                 claireLamsdale,
    //                 wolfgangbiederstaedt,
    //                 hellmutSchwarz
    //             },
    //             Categories = new HashSet<Category> {
    //                 schoolCategory
    //             },
    //             Language = english
    //         };
    //         var book7 = new Book
    //         {
    //             InternationalStandardBookNumber = "9783125380011",
    //             Title = "¡Adelante! Nivel intermedio",
    //             SubTitle = "¡Adelante! Schülerbuch 11./12. Schuljahr. Nivel intermedio",
    //             Description = "Alltagssituationen, über die man gerne spricht - Schulung der Grundfertigkeiten - Förderung des Sprechens - Aufgabenorientiertes Arbeiten - Grammatik, die auf das Wesentliche reduziert ist - Anregungen zum selbstständigen Lernen 8 Unidades, linearer Buchaufbau mit 3 Plateauphasen. Die inhaltliche Konzeption des Nivel elemental wird fortgeführt: Am Ende jeder Unidad steht ebenfalls die Tarea final. Die Schülerinnen und Schüler lernen interessante Schauplätze in Spanien und Lateinamerika kennen. Intensive Abiturvorbereitung im Teil Preparación para los exámenes: ein Angebot verschiedener…mehr",
    //             Publisher = ernstKlett,
    //             Authors = new HashSet<Author> {
    //                 antonioBarquero,
    //                 julianaBizama,
    //                 jaimeCorpas,
    //                 pedroCrovetto,
    //                 evaDiaz,
    //                 claireMarieJeske,
    //                 aliciaJimenez,
    //                 javierNavarro,
    //                 rosamnaPardellas
    //             },
    //             Categories = new HashSet<Category> {
    //                 schoolCategory
    //             },
    //             Language = spain
    //         };

    //         /* Add Books */
    //         context.Books.Add(book1);
    //         context.Books.Add(book2);
    //         context.Books.Add(book3);
    //         context.Books.Add(book4);
    //         context.Books.Add(book5);
    //         context.Books.Add(book6);
    //         context.Books.Add(book7);

    //         /* Add languages */
    //         context.Languages.Add(english);
    //         context.Languages.Add(german);
    //         context.Languages.Add(russian);
    //         context.Languages.Add(italian);
    //         context.Languages.Add(spain);

    //         /* Add categories */
    //         context.Categories.Add(computerCategory);

    //         /* BookItems */
    //         var bookItem1 = new BookItem
    //         {
    //             Book = book1,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 1,
    //                 Rack = 1,
    //                 Level = 1,
    //                 Position = 1,
    //             },
    //         };
    //         var bookItem2 = new BookItem
    //         {
    //             Book = book1,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 1,
    //                 Rack = 1,
    //                 Level = 1,
    //                 Position = 2,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem3 = new BookItem
    //         {
    //             Book = book2,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 1,
    //                 Rack = 1,
    //                 Level = 2,
    //                 Position = 2,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem4 = new BookItem
    //         {
    //             Book = book3,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 2,
    //                 Corridor = 1,
    //                 Rack = 1,
    //                 Level = 1,
    //                 Position = 2,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem5 = new BookItem
    //         {
    //             Book = book3,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 6,
    //                 Rack = 1,
    //                 Level = 1,
    //                 Position = 1,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem6 = new BookItem
    //         {
    //             Book = book4,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 1,
    //                 Rack = 1,
    //                 Level = 2,
    //                 Position = 3,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem7 = new BookItem
    //         {
    //             Book = book4,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 1,
    //                 Rack = 1,
    //                 Level = 2,
    //                 Position = 4,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem8 = new BookItem
    //         {
    //             Book = book4,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 1,
    //                 Rack = 1,
    //                 Level = 2,
    //                 Position = 5,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem9 = new BookItem
    //         {
    //             Book = book5,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 3,
    //                 Rack = 1,
    //                 Level = 1,
    //                 Position = 2,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem10 = new BookItem
    //         {
    //             Book = book6,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 4,
    //                 Rack = 1,
    //                 Level = 1,
    //                 Position = 2,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem11 = new BookItem
    //         {
    //             Book = book7,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 2,
    //                 Rack = 3,
    //                 Level = 1,
    //                 Position = 2,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };
    //         var bookItem12 = new BookItem
    //         {
    //             Book = book7,
    //             StockLocation = new StockLocation
    //             {
    //                 Hall = 1,
    //                 Corridor = 2,
    //                 Rack = 3,
    //                 Level = 1,
    //                 Position = 3,
    //             },
    //             RentedByUserId = null,
    //             RentedAt = null,
    //         };

    //         context.BookItems.Add(bookItem1);
    //         context.BookItems.Add(bookItem2);
    //         context.BookItems.Add(bookItem3);
    //         context.BookItems.Add(bookItem4);
    //         context.BookItems.Add(bookItem5);
    //         context.BookItems.Add(bookItem6);
    //         context.BookItems.Add(bookItem7);
    //         context.BookItems.Add(bookItem8);
    //         context.BookItems.Add(bookItem9);
    //         context.BookItems.Add(bookItem10);
    //         context.BookItems.Add(bookItem11);
    //         context.BookItems.Add(bookItem12);

    //         school1.BookItems.Add(bookItem1);
    //         school1.BookItems.Add(bookItem2);
    //         school1.BookItems.Add(bookItem3);
    //         school1.BookItems.Add(bookItem4);
    //         school1.BookItems.Add(bookItem5);
    //         school1.BookItems.Add(bookItem6);
    //         school2.BookItems.Add(bookItem7);
    //         school2.BookItems.Add(bookItem8);
    //         school2.BookItems.Add(bookItem9);
    //         school2.BookItems.Add(bookItem10);
    //         school2.BookItems.Add(bookItem11);
    //         school2.BookItems.Add(bookItem12);

    //         context.Schools.Add(school1);
    //         context.Schools.Add(school2);

    //         context.SaveChanges();

    //         var user1 = new ApplicationUser
    //         {
    //             UserName = "admin@athene.com",
    //             Email =  "admin@athene.com",
    //             Surname = "Max",
    //             Lastname = "Mustermann",
    //             Birthsday = new DateTime(1988, 1, 1),
    //         };
    //         user1.Claims.Add(new IdentityUserClaim<string>
    //         {
    //             ClaimType = ClaimTypes.Role,
    //             ClaimValue = "Librarian"
    //         });
    //         user1.Claims.Add(new IdentityUserClaim<string>
    //         {
    //             ClaimType = ClaimTypes.Role,
    //             ClaimValue = "Administrator"
    //         });
    //         var user2 = new ApplicationUser
    //         {
    //             UserName = "u.musterfrau@athene.com",
    //             Email =  "u.musterfrau@athene.com",
    //             Surname = "Uwe",
    //             Lastname = "Musterfrau",
    //             Birthsday = new DateTime(1989, 1, 1),
    //         };
    //         user2.Claims.Add(new IdentityUserClaim<string>
    //         {
    //             ClaimType = ClaimTypes.Role,
    //             ClaimValue = "Librarian"
    //         });

    //         /* students */
    //         var student1 = new ApplicationUser
    //         {
    //             UserName = "teststudent1@athene.com",
    //             Email =  "teststudent1@athene.com",
    //             Surname = "Rudolf",
    //             Lastname = "Mustermann",
    //             Gender = Gender.Male,
    //             Birthsday = new DateTime(1988, 1, 1),
    //             Student = new Student {
    //                StudentId = "43245BD",
    //                SchoolClass = schoolClass1
    //             },
    //         };
    //         student1.Claims.Add(new IdentityUserClaim<string>
    //         {
    //             ClaimType = ClaimTypes.Role,
    //             ClaimValue = "Student"
    //         });
    //         var student2 = new ApplicationUser
    //         {
    //             UserName = "teststudent2@athene.com",
    //             Email =  "teststudent2@athene.com",
    //             Surname = "Anna",
    //             Lastname = "Mustermann",
    //             Gender = Gender.Female,
    //             Birthsday = new DateTime(1978, 3, 1),
    //             Student = new Student {
    //                StudentId = "43235BD",
    //                SchoolClass = schoolClass3
    //             },
    //         };
    //         student2.Claims.Add(new IdentityUserClaim<string>
    //         {
    //             ClaimType = ClaimTypes.Role,
    //             ClaimValue = "Student"
    //         });

    //         var res1 = userManager.CreateAsync(user1, "Admin123!").Result;
    //         var res2 = userManager.CreateAsync(user2, "Test123!").Result;
    //         var res3 = userManager.CreateAsync(student1, "Test123!").Result;
    //         var res4 = userManager.CreateAsync(student2, "Test123!").Result;

    //         /* set rented books */
    //         bookItem1.RentedByUserId = user1.Id;
    //         bookItem1.RentedAt = DateTime.Now;
    //         bookItem2.RentedByUserId = student1.Id;
    //         bookItem2.RentedAt = DateTime.Now;
    //         context.SaveChanges();
    //     }
    // }
}
