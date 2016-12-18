using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athene.Inventory.Web.Models
{
    public class Book
    {
        public Book() 
        {
            this.Authors = new HashSet<Author>();
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
        public string InternationalStandardBookNumber { get; set; }
        [Required]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Author> Authors { get; set; }
        [Required]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
