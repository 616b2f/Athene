using System;
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
            this.OwnedBooks = new HashSet<BookItem>();
            this.Categories = new HashSet<Category>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name="Titel")]
        public string Title { get; set; }
        [Display(Name="Untertitel")]
        public string SubTitle { get; set; }
        [Display(Name="Beschreibung")]
        public string Description { get; set; }
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
        [Display(Name="ISBN")]
        public string InternationalStandardBookNumber { get; set; }
        [Required]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        [Display(Name="Verlag")]
        public Publisher Publisher { get; set; }
        [DataType(DataType.Date)]
        [Display(Name="Erscheinungsdatum")]
        public DateTime? PublishedAt { get; set; }
        [Display(Name="Autoren")]
        public ICollection<Author> Authors { get; set; }
        public ICollection<Category> Categories { get; set; }
        [Required]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        [Display(Name="Sprache")]
        public Language Language { get; set; }
        [Display(Name="Standplätze")]
        public ICollection<BookItem> OwnedBooks { get; set; }
    }
}
