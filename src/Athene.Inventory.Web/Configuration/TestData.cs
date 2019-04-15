using System;
using System.Collections.Generic;
using System.Security.Claims;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Abstractions.Utils;
using Athene.Inventory.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Athene.Inventory.Web
{
    public static class TestData
    {
        public static void Initialize(IServiceProvider provider)
        {
            var _userManager = provider.GetService<UserManager<ApplicationUser>>();
            var _articleRepo = provider.GetService<IArticleRepository>();
            var _bookMetaRepo = provider.GetService<IBookMetaRepository>();
            var _inventory = provider.GetService<IInventoryRepository>();
            TestData.CreateUsers(_userManager);
            TestData.CreateBookMeta(_bookMetaRepo);
            TestData.CreateBookArticles(_articleRepo, _bookMetaRepo);
            TestData.CreateBookInventoryItems(_inventory, _articleRepo);
        }

        public static void CreateUsers(UserManager<ApplicationUser> userManager)
        {
            /* Board of education */
            var boardOfEducation = new BoardOfEducation("Landrat");

            /* School addresses */
            var schhol1Address = new Address("Musterstraße 1", "88451", "Dettingen a. d. Iller", "DE");
            var schhol2Address = new Address("Musterstraße 2", "88451", "Dettingen a. d. Iller", "DE");

            /* Schools */
            var school1 = new School("Schule 1", "SC1", schhol1Address, boardOfEducation);
            var school2 = new School("Schule 2", "SC2", schhol2Address, boardOfEducation);

            /* school classes */
            var schoolClass1 = new SchoolClass("BF1", school1);
            var schoolClass2 = new SchoolClass("BF2", school1);
            var schoolClass3 = new SchoolClass("BR2", school2);
            var schoolClass4 = new SchoolClass("BE2", school2);
            school1.SchoolClasses.Add(schoolClass1);
            school1.SchoolClasses.Add(schoolClass2);
            school2.SchoolClasses.Add(schoolClass3);
            school2.SchoolClasses.Add(schoolClass4);

            var adminUser = new ApplicationUser
            {
                UserName = "admin@athene.com",
                Email =  "admin@athene.com",
                Surname = "Max",
                Lastname = "Mustermann",
                Birthsday = new DateTime(1988, 1, 1),
            };
            var librarianUser = new ApplicationUser
            {
                UserName = "u.musterfrau@athene.com",
                Email =  "u.musterfrau@athene.com",
                Surname = "Uwe",
                Lastname = "Musterfrau",
                Birthsday = new DateTime(1989, 1, 1),
            };

            /* students */
            var student1 = new ApplicationUser
            {
                UserName = "teststudent1@athene.com",
                Email =  "teststudent1@athene.com",
                Surname = "Rudolf",
                Lastname = "Mustermann",
                Gender = Gender.Male,
                Birthsday = new DateTime(1988, 1, 1),
                Student = new Student {
                   StudentId = "43245BD",
                   SchoolClass = schoolClass1
                },
            };
            var student2 = new ApplicationUser
            {
                UserName = "teststudent2@athene.com",
                Email =  "teststudent2@athene.com",
                Surname = "Anna",
                Lastname = "Mustermann",
                Gender = Gender.Female,
                Birthsday = new DateTime(1978, 3, 1),
                Student = new Student {
                   StudentId = "43235BD",
                   SchoolClass = schoolClass3
                },
            };
            var student3 = new ApplicationUser
            {
                UserName = "teststudent3@athene.com",
                Email =  "teststudent3@athene.com",
                Surname = "Simon",
                Lastname = "Mustermann",
                Gender = Gender.Female,
                Birthsday = new DateTime(1979, 6, 1),
                Student = new Student {
                   StudentId = "43335BD",
                   SchoolClass = schoolClass3
                },
            };

            var librarianRole = new Claim(ClaimTypes.Role, "Librarian");
            var adminRole = new Claim(ClaimTypes.Role, "Administrator");
            var studentRole = new Claim(ClaimTypes.Role, "Student");

            var dataImportBooksPermission = new Claim(Constants.ClaimTypes.Permission, Constants.Permissions.DataImportBooks);
            var dataImportUsersPermission = new Claim(Constants.ClaimTypes.Permission, Constants.Permissions.DataImportUsers);
            var dataImportInventoryItemsPermission = new Claim(Constants.ClaimTypes.Permission, Constants.Permissions.DataImportInventoryItems);
            var rentBookPermission = new Claim(Constants.ClaimTypes.Permission, Constants.Permissions.RentBooks);
            var dataImportPermission = new Claim(Constants.ClaimTypes.Permission, Constants.Permissions.DataImport);
            var administrateInventoryPermission = new Claim(Constants.ClaimTypes.Permission, Constants.Permissions.AdministrateInventory);

            userManager.CreateAsync(adminUser, "Admin123!").Wait();
            userManager.CreateAsync(librarianUser, "Test123!").Wait();
            userManager.CreateAsync(student1, "Test123!").Wait();
            userManager.CreateAsync(student2, "Test123!").Wait();
            userManager.CreateAsync(student3, "Test123!").Wait();

            userManager.AddClaimAsync(adminUser, adminRole).Wait();
            userManager.AddClaimAsync(adminUser, dataImportPermission).Wait();
            userManager.AddClaimAsync(adminUser, dataImportBooksPermission).Wait();
            userManager.AddClaimAsync(adminUser, dataImportUsersPermission).Wait();
            userManager.AddClaimAsync(adminUser, dataImportInventoryItemsPermission).Wait();
            userManager.AddClaimAsync(adminUser, administrateInventoryPermission).Wait();
            
            userManager.AddClaimAsync(librarianUser, librarianRole).Wait();
            userManager.AddClaimAsync(librarianUser, dataImportBooksPermission).Wait();
            userManager.AddClaimAsync(librarianUser, administrateInventoryPermission).Wait();

            userManager.AddClaimAsync(student1, studentRole).Wait();
            userManager.AddClaimAsync(student1, rentBookPermission).Wait();

            userManager.AddClaimAsync(student2, studentRole).Wait();
            userManager.AddClaimAsync(student2, rentBookPermission).Wait();
        }

        public static void CreateBookMeta(IBookMetaRepository bookMetaRepo)
        {
            var prenticeHall = new Publisher { Id = 1, Name = "Prentice Hall" };
            var pearson = new Publisher { Id = 2, Name = "Pearson" };
            var thomsonReuters = new Publisher { Id = 3, Name = "Thomson Reuters" };
            var relxGroup = new Publisher { Id = 4, Name = "RELX Group" };
            var penguinRandomHouse = new Publisher { Id = 5, Name = "Penguin Random House" };
            var woltersKluwer = new Publisher { Id = 6, Name = "Wolters Kluwer" };
            var rheinwerk = new Publisher { Id = 7, Name = "Rheinwerk" };
            var hanserVerlag = new Publisher { Id = 8, Name = "Der Carl Hanser Verlag" };
            var cornelsen = new Publisher { Id = 9, Name = "Cornelsen Verlag GmbH" };
            var ernstKlett = new Publisher { Id = 10, Name = "Ernst Klett Verlag GmbH" };
            bookMetaRepo.AddPublisher(prenticeHall);
            bookMetaRepo.AddPublisher(pearson);
            bookMetaRepo.AddPublisher(thomsonReuters);
            bookMetaRepo.AddPublisher(relxGroup);
            bookMetaRepo.AddPublisher(penguinRandomHouse);
            bookMetaRepo.AddPublisher(woltersKluwer);
            bookMetaRepo.AddPublisher(rheinwerk);
            bookMetaRepo.AddPublisher(hanserVerlag);
            bookMetaRepo.AddPublisher(cornelsen);
            bookMetaRepo.AddPublisher(ernstKlett);

            var robertCMartin = new Author { Id = 1, FullName = "Robert C. Martin" };
            var deanWampler = new Author { Id = 2, FullName = "Dean Wampler" };
            var benAaronovitch = new Author { Id = 3, FullName = "Ben Aaronovitch" };
            var atiaAbawi = new Author { Id = 4, FullName = "Atia Abawi" };
            var davidAbbott = new Author { Id = 5, FullName = "David Abbott" };
            var emmyAbrahamson = new Author { Id = 6, FullName = "Emmy Abrahamson" };
            var sashaAbramsky = new Author { Id = 7, FullName = "Sasha Abramsky" };
            var thomasClaudiusHuber = new Author { Id = 8, FullName = "Thomas Claudius Huber" };
            var andrewTannenbaum = new Author { Id = 9, FullName = "Andrew S. Tannenbaum" };
            var herbertBos = new Author { Id = 10, FullName = "Herbert Bos" };
            var rolfSocher = new Author { Id = 11, FullName = "Rolf Socher" };
            var susanAbbey = new Author { Id = 12, FullName = "Susan Abbey" };
            var roderickCox = new Author { Id = 13, FullName = "Roderick Cox" };
            var laurenceHarger = new Author { Id = 14, FullName = "Laurnce Harger" };
            var claireLamsdale = new Author { Id = 15, FullName = "Claire Lemsdale" };
            var wolfgangBiederstaedt = new Author { Id = 16, FullName = "Wolfgang Biederstädt" };
            var hellmutSchwarz = new Author { Id = 17, FullName = "Hellmut Schwarz" };
            var antonioBarquero = new Author { Id = 18, FullName = "Antonio Barquero" };
            var julianaBizama = new Author { Id = 19, FullName = "Juliana Bizama" };
            var jaimeCorpas = new Author { Id = 20, FullName = "Jaime Corpas" };
            var pedroCrovetto = new Author { Id = 21, FullName = "Pedro Crovetto-Bizama" };
            var evaDiaz = new Author { Id = 22, FullName = "Eva Diaz" };
            var claireMarieJeske = new Author { Id = 23, FullName = "Claire-Marie Jeske" };
            var aliciaJimenez = new Author { Id = 24, FullName = "Alicia Jimenez Romera" };
            var javierNavarro = new Author { Id = 25, FullName = "Javier Navarro" };
            var rosamnaPardellas = new Author { Id = 26, FullName = "Rosamna Pradellas Velay" };
            bookMetaRepo.AddAuthors(new []{ 
                robertCMartin,
                deanWampler,
                benAaronovitch,
                atiaAbawi,
                davidAbbott,
                emmyAbrahamson,
                sashaAbramsky,
                thomasClaudiusHuber,
                andrewTannenbaum,
                herbertBos,
                rolfSocher,
                susanAbbey,
                roderickCox,
                laurenceHarger,
                claireLamsdale,
                wolfgangBiederstaedt,
                hellmutSchwarz,
                antonioBarquero,
                julianaBizama,
                jaimeCorpas,
                pedroCrovetto,
                evaDiaz,
                claireMarieJeske,
                aliciaJimenez,
                javierNavarro,
                rosamnaPardellas,
            });

            var computerCategory = new Category { Id = 1, Name = "Computers" };
            var thrillerCategory = new Category { Id = 2, Name = "Thrillers" };
            var specializedCategory = new Category { Id = 3, Name = "Fachbuch" };
            var literatureCategory = new Category { Id = 4, Name = "Literatur" };
            var humorCategory = new Category { Id = 5, Name = "Humor" };
            var codingCategory = new Category { Id = 6, Name = "Programmieren" };
            var schoolCategory = new Category { Id = 7, Name = "Schulbücher" };
            bookMetaRepo.AddCategories(new [] {
                computerCategory,
                thrillerCategory,
                specializedCategory,
                literatureCategory,
                humorCategory,
                codingCategory,
                schoolCategory,
            });


            var english = new Language { Id = 1, Name = "English" };
            var german = new Language { Id = 2, Name = "Deutsch" };
            var russian = new Language { Id = 3, Name = "Russian" };
            var spain = new Language { Id = 4, Name = "Spanisch" };
            var italian = new Language { Id = 5, Name = "Italienisch" };
            bookMetaRepo.AddLanguages(new [] {
                english,
                german,
                russian,
                spain,
                italian,
            });
        }

        public static void CreateBookArticles(IArticleRepository articleRepo, IBookMetaRepository bookMetaRepo)
        {
            var prenticeHall = bookMetaRepo.FindPublisherById(1);
            var pearson = bookMetaRepo.FindPublisherById(2);
            var thomsonReuters = bookMetaRepo.FindPublisherById(3);
            var relxGroup = bookMetaRepo.FindPublisherById(4);
            var penguinRandomHouse = bookMetaRepo.FindPublisherById(5);
            var woltersKluwer = bookMetaRepo.FindPublisherById(6);
            var rheinwerk = bookMetaRepo.FindPublisherById(7);
            var hanserVerlag = bookMetaRepo.FindPublisherById(8);
            var cornelsen = bookMetaRepo.FindPublisherById(9);
            var ernstKlett = bookMetaRepo.FindPublisherById(10);

            var robertCMartin = bookMetaRepo.FindAuthorById(1);
            var deanWampler = bookMetaRepo.FindAuthorById(2);
            var thomasClaudiusHuber = bookMetaRepo.FindAuthorById(8);
            var andrewTannenbaum = bookMetaRepo.FindAuthorById(9);
            var herbertBos = bookMetaRepo.FindAuthorById(10);
            var rolfSocher = bookMetaRepo.FindAuthorById(11);
            var susanAbbey = bookMetaRepo.FindAuthorById(12);
            var roderickCox = bookMetaRepo.FindAuthorById(13);
            var laurenceHarger = bookMetaRepo.FindAuthorById(14);
            var claireLamsdale = bookMetaRepo.FindAuthorById(15);
            var wolfgangBiederstaedt = bookMetaRepo.FindAuthorById(16);
            var hellmutSchwarz = bookMetaRepo.FindAuthorById(17);
            var antonioBarquero = bookMetaRepo.FindAuthorById(18);
            var julianaBizama = bookMetaRepo.FindAuthorById(19);
            var jaimeCorpas = bookMetaRepo.FindAuthorById(20);
            var pedroCrovetto = bookMetaRepo.FindAuthorById(21);
            var evaDiaz = bookMetaRepo.FindAuthorById(22);
            var claireMarieJeske = bookMetaRepo.FindAuthorById(23);
            var aliciaJimenez = bookMetaRepo.FindAuthorById(24);
            var javierNavarro = bookMetaRepo.FindAuthorById(25);
            var rosamnaPardellas = bookMetaRepo.FindAuthorById(26);

            var computerCategory = bookMetaRepo.FindCategoryById(1);
            var thrillerCategory = bookMetaRepo.FindCategoryById(2);
            var specializedCategory = bookMetaRepo.FindCategoryById(3);
            var literatureCategory = bookMetaRepo.FindCategoryById(4);
            var humorCategory = bookMetaRepo.FindCategoryById(5);
            var codingCategory = bookMetaRepo.FindCategoryById(6);
            var schoolCategory = bookMetaRepo.FindCategoryById(7);

            var english = bookMetaRepo.FindLanguageById(1);
            var german = bookMetaRepo.FindLanguageById(2);
            var russian = bookMetaRepo.FindLanguageById(3);
            var spain = bookMetaRepo.FindLanguageById(4);
            var italian = bookMetaRepo.FindLanguageById(5);

            var book1 = new Book
            {
                Id = 1,
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
                Id = 2,
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
                    literatureCategory
                },
                Language = english
            };
            var book3 = new Book
            {
                Id = 3,
                InternationalStandardBookNumber = "9783836219563",
                Title = "Windows Presentation Forms",
                SubTitle = "WPF - Das umfassende Handbuch",
                Description = "Geballtes Wissen zum Grafik-Framework von .NET! Ob Grundlagen, XAML, GUI-Entwicklung, Datenbindung, Animationen, Multimedia oder Migration - hier finden Sie auf jede Frage eine Antwort! Grundkenntnisse in C# vorausgesetzt, ist dieses Buch sowohl zum Einstieg als auch als Nachschlagewerk optimal geeignet.",
                Publisher = rheinwerk,
                Authors = new HashSet<Author> {
                    thomasClaudiusHuber,
                },
                Categories = new HashSet<Category> {
                    computerCategory,
                    codingCategory,
                    literatureCategory
                },
                Language = german
            };
            var book4 = new Book
            {
                Id = 4,
                InternationalStandardBookNumber = "9783836219563",
                Title = "Moderne Betriebssysteme",
                SubTitle = "Moderne Betriebssysteme (4., aktualisierte Auflage)",
                Description = "Auch in dieser komplett überarbeiteten Neuauflage des preisgekrönten Lehrbuchs stellen die Autoren wie gewohnt auf unterhaltsame Art und Weise alle Konzepte rund um das Thema Betriebssysteme vor, die man benötigt, um moderne Betriebssysteme zu verstehen und zu entwickeln. Dabei wurden den neuesten Entwicklungen der Betriebssysteme sowie der zugrunde liegenden Hardware Rechnung getragen. Das Lehrbuch enthält umfangreiche Aktualisierungen zu UNIX, Linux und Windows und behandelt erstmals auch Android als mobiles Betriebssystem. Das Kapitel zu IT-Sicherheit wurde grundlegend aktualisiert, ein neues Kapitel behandelt die Themen Virtualisierungs- und Cloud-Technologie.  Zahlreiche Abbildungen, viele Beispiele, aktuelle Fallstudien sowie über 500 Übungsaufgaben erleichtern das Verstehen und Erlernen der vorgestellten Konzepte und Theorien. Zudem stehen auf den Companion Websites zum Buch Experimentier- und Simulationswerkzeuge zu Linux und Window bereit – ideal zum Selbststudium.",
                Publisher = rheinwerk,
                Authors = new HashSet<Author> {
                    andrewTannenbaum,
                    herbertBos
                },
                Categories = new HashSet<Category> {
                    computerCategory,
                    literatureCategory
                },
                Language = german
            };
            var book5 = new Book
            {
                Id = 5,
                InternationalStandardBookNumber = "9783446414457",
                Title = "Theoretische Grundlagen der Informatik",
                SubTitle = "",
                Description = "Das Buch bietet einen Einstieg in die theoretischen Grundlagen der Informatik. Es beschränkt sich auf die klassischen Themen: formale Sprachen, endliche Automaten und Grammatiken, Turing-Maschinen, Berechenbarkeit und Entscheidbarkeit, Komplexität. Das Konzept der Transformation zwischen den verschiedenen Formalismen zieht sich wie ein roter Faden durch das gesamte Buch. Auf eine anschauliche Vermittlung der Begriffe und Methoden der theoretischen Informatik und ihre Vertiefung in Aufgaben und Programmierprojekten wird großer Wert gelegt. Auf der zu dem Buch gehörenden Website findet sich das Lernprogramm Machines, mit dem endliche Automaten, Kellerautomaten, Grammatiken, reguläre Ausdrücke und Turing-Maschinen mit einer komfortablen grafischen Oberfläche realisiert und visualisiert werden können.",
                Publisher = hanserVerlag,
                Authors = new HashSet<Author> {
                    rolfSocher
                },
                Categories = new HashSet<Category> {
                    computerCategory,
                    literatureCategory
                },
                Language = german
            };
            var book6 = new Book
            {
                Id = 6,
                InternationalStandardBookNumber = "9783060313150",
                Title = "English G 21",
                SubTitle = "",
                Description = "Eine systematische Prüfungsvorbereitung mit drei kürzeren Units, die dem Aufbau von Band 5 folgen",
                Publisher = cornelsen,
                Authors = new HashSet<Author> {
                    susanAbbey,
                    roderickCox,
                    laurenceHarger,
                    claireLamsdale,
                    wolfgangBiederstaedt,
                    hellmutSchwarz
                },
                Categories = new HashSet<Category> {
                    schoolCategory
                },
                Language = english
            };
            var book7 = new Book
            {
                Id = 7,
                InternationalStandardBookNumber = "9783125380011",
                Title = "¡Adelante! Nivel intermedio",
                SubTitle = "¡Adelante! Schülerbuch 11./12. Schuljahr. Nivel intermedio",
                Description = "Alltagssituationen, über die man gerne spricht - Schulung der Grundfertigkeiten - Förderung des Sprechens - Aufgabenorientiertes Arbeiten - Grammatik, die auf das Wesentliche reduziert ist - Anregungen zum selbstständigen Lernen 8 Unidades, linearer Buchaufbau mit 3 Plateauphasen. Die inhaltliche Konzeption des Nivel elemental wird fortgeführt: Am Ende jeder Unidad steht ebenfalls die Tarea final. Die Schülerinnen und Schüler lernen interessante Schauplätze in Spanien und Lateinamerika kennen. Intensive Abiturvorbereitung im Teil Preparación para los exámenes: ein Angebot verschiedener…mehr",
                Publisher = ernstKlett,
                Authors = new HashSet<Author> {
                    antonioBarquero,
                    julianaBizama,
                    jaimeCorpas,
                    pedroCrovetto,
                    evaDiaz,
                    claireMarieJeske,
                    aliciaJimenez,
                    javierNavarro,
                    rosamnaPardellas
                },
                Categories = new HashSet<Category> {
                    schoolCategory
                },
                Language = spain
            };

            var books = new [] {
                book1,
                book2,
                book3,
                book4,
                book5,
                book6,
                book7,
            };
            articleRepo.AddArticles(books);
        }

        public static void CreateBookInventoryItems(IInventoryRepository inventory, IArticleRepository articleRepo)
        {
            var book1 = articleRepo.FindArticleById(1);
            var book2 = articleRepo.FindArticleById(2);
            var book3 = articleRepo.FindArticleById(3);
            var book4 = articleRepo.FindArticleById(4);
            var book5 = articleRepo.FindArticleById(5);
            var book6 = articleRepo.FindArticleById(6);
            var book7 = articleRepo.FindArticleById(7);

            var sl1 = new StockLocation { Hall = "1", Corridor = "1", Rack = "1", Level = "1", Position = "1", };
            var sl2 = new StockLocation { Hall = "1", Corridor = "1", Rack = "1", Level = "1", Position = "2", };

            var inv1 = new InventoryItem 
            {
                Id = 1,
                Barcode = "DDC8C0EC7E0B4EB094A058762AC38FE7",
                Article = book1,
                PurchasedAt = new DateTime(2016, 3, 20),
                Condition = Condition.LikeNew,
                StockLocation = sl1
            };
            var inv2 = new InventoryItem 
            {
                Id = 2,
                Barcode = "DC71308FA060404C847B87A1C9D18533",
                Article = book1,
                PurchasedAt = new DateTime(2016, 3, 20),
                Condition = Condition.VeryGood,
                StockLocation = sl1
            };
            var inv3 = new InventoryItem 
            {
                Id = 3,
                Barcode = "185CCEB1353E4C2A95894C540C756722",
                Article = book2,
                PurchasedAt = new DateTime(2016, 3, 20),
                Condition = Condition.LikeNew,
                StockLocation = sl2
            };

            inventory.AddInventoryItems(new [] 
            {
                inv1,
                inv2,
                inv3,
            });
        }
    }
}
