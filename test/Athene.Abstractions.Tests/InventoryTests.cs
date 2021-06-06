using Xunit;
using System.Collections.Generic;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Abstractions.TestImp;

namespace Athene.Inventory.AbstractionsTests
{
    public class InventoryTests
    {
        private readonly IArticleRepository _articles;
        private readonly IInventoryRepository _inventory;
        public InventoryTests()
        {
            _articles = new InMemoryArticleRepository();
            _inventory = new InMemoryInventoryRepository();
        }

        [Fact]
        private void AddBook()
        {
            var book = new Book
            {
                ArticleId = 1,
                InternationalStandardBookNumber = "9780132350884",
                Title = "Clean Code",
                SubTitle = "A Handbook of Agile Software Craftsmanship",
                Description = "Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer–but only if you work at it.",
                Publisher = new Publisher {
                    Id = 1,
                    Name = "Prentice Hall",
                },
                Authors = new List<Author> {
                    new Author { 
                        Id = 1,
                        FullName = "Robert C. Martin",
                        Info = "aka. Oncle Bob",
                    },
                    new Author { 
                        Id = 2,
                        FullName = "Dean Wampler",
                        Info = "",
                    },
                },
                Categories = new List<Category> {
                    new Category {
                       Id = 1,
                       Name = "Computers", 
                    },
                },
                Language = new Language { 
                    Name = "English" 
                },
            };
            _articles.AddArticle(book);
            var article = _articles.FindArticleById(1);

            Assert.True(article is Book);
            var tmpBook = article as Book;
            Assert.Equal(book.ArticleId, tmpBook.ArticleId);
            Assert.Equal(book.Name, tmpBook.Name);
            Assert.Equal(book.Title, tmpBook.Title);
            Assert.Equal(book.SubTitle, tmpBook.SubTitle);
            Assert.Equal(book.InternationalStandardBookNumber, tmpBook.InternationalStandardBookNumber);
            Assert.Equal(book.Description, tmpBook.Description);
            Assert.True(tmpBook.Publisher != null);
            Assert.Equal(book.Publisher.Name ,tmpBook.Publisher.Name);
            Assert.True(tmpBook.Authors.Count == 2);
            Assert.True(tmpBook.Categories.Count == 1);
            Assert.True(tmpBook.Language != null);
            Assert.Equal(book.Language.Name, tmpBook.Language.Name);
        }

        [Fact]
        public void RentBook()
        {
            var inventoryItem = new InventoryItem
            {
                Id = 1,
                Article = new Book
                {
                    ArticleId = 1,
                    InternationalStandardBookNumber = "9780132350884",
                    Title = "Clean Code",
                    SubTitle = "A Handbook of Agile Software Craftsmanship",
                    Description = "Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer–but only if you work at it.",
                    Publisher = new Publisher {
                        Id = 1,
                        Name = "Prentice Hall",
                    },
                    Authors = new List<Author> {
                        new Author { 
                            Id = 1,
                            FullName = "Robert C. Martin",
                            Info = "aka. Oncle Bob",
                        },
                        new Author { 
                            Id = 2,
                            FullName = "Dean Wampler",
                            Info = "",
                        },
                    },
                    Categories = new List<Category> {
                        new Category {
                        Id = 1,
                        Name = "Computers", 
                        },
                    },
                    Language = new Language { 
                        Name = "English" 
                    },
                },
                StockLocation = new StockLocation 
                {
                    Hall = "1",
                    Corridor = "2",
                    Rack = "3",
                    Level = "4",
                    Position = "5",
                },
            };
            _inventory.AddInventoryItem(inventoryItem);
            var tmpInventoryItem = _inventory.FindInventoryItemById(1);

            Assert.Equal(1 ,tmpInventoryItem.Article.ArticleId);
            Assert.True(tmpInventoryItem.Article is Book);
            var book = tmpInventoryItem.Article as Book;
            Assert.Equal("9780132350884", book.InternationalStandardBookNumber);
            Assert.Equal(1, tmpInventoryItem.Id);
            Assert.Equal("1", tmpInventoryItem.StockLocation.Hall);
            Assert.Equal("2", tmpInventoryItem.StockLocation.Corridor);
            Assert.Equal("3", tmpInventoryItem.StockLocation.Rack);
            Assert.Equal("4", tmpInventoryItem.StockLocation.Level);
            Assert.Equal("5", tmpInventoryItem.StockLocation.Position);
        }
    }
}
