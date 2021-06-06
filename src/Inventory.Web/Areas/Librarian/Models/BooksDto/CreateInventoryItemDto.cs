using System;
using System.ComponentModel.DataAnnotations;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.BooksDto
{
    public class CreateInventoryItemDto
    {
        [Required]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        [Required]
        [Display(Name="Halle")]
        public string Hall { get; set; }
        [Required]
        [Display(Name="Gang")]
        public string Corridor { get; set; }
        [Required]
        [Display(Name="Regal")]
        public string Rack { get; set; }
        [Required]
        [Display(Name="Ebene")]
        public string Level { get; set; }
        [Required]
        [Display(Name="Position")]
        public string Position { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name="Kaufdatum")]
        public DateTime? PurchasedAt { get; set; }
        [Required]
        [Display(Name="Zustand")]
        public Condition Condition { get; set; }

        [Display(Name="Notiz")]
        public string Note { get; set; }

    }
}
