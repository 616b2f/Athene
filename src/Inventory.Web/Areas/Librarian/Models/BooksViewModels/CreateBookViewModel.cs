using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Athene.Abstractions.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.BooksViewModels
{
    public class CreateBookViewModel
    {
        public CreateBookViewModel() {
            this.Authors = new HashSet<Author>();
        }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
        public string InternationalStandardBookNumber { get; set; }
        public ICollection<Author> Authors { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public int[] authorsIds { get; set; }
        [Required]
        public int[] categoriesIds { get; set; }
    }
}
