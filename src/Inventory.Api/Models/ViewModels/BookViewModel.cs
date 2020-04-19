using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athene.Inventory.Web.ViewModels
{
    public class BookViewModel
    {
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
        public string PublisherName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name="Erscheinungsdatum")]
        public DateTime? PublishedAt { get; set; }
        [Display(Name="Autoren")]
        public int[] AuthorIds { get; set; }
        public string[] Categories { get; set; }
        [Required]
        [Display(Name="Sprache")]
        public int LanguageId { get; set; }
    }
}
