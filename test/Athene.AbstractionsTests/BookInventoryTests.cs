using Xunit;

namespace Athene.AbstractionsTests
{
    public class BookInventoryTests
    {
        private readonly IBookInventory _bookInventory;
        public BookInventoryTests()
        {
            _bookInventory = new InMemoryBookInventory();
        }
        private void AddBook()
        {
            var book = new Book
            {
                Id = 1,
                InternationalStandardBookNumber = "9780132350884",
                Title = "Clean Code",
                SubTitle = "A Handbook of Agile Software Craftsmanship",
                Description = "Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship . Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code “on the fly” into a book that will instill within you the values of a software craftsman and make you a better programmer–but only if you work at it.",
                Publisher = new Publisher {
                    Id = 1,
                    Name = "Prentice Hall",
                },
                Authors = new HashSet<Author> {
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
                Categories = new HashSet<Category> {
                    new Category {
                       Id = 1,
                       Name = "Computers", 
                    },
                },
                Language = new Language("English"),
            };
            _bookInventory.AddBook(book);
            _bookInventory.
        }
    }
}