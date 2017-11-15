using System;
using System.ComponentModel.DataAnnotations;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.BooksViewModels
{
    public class CreateBookItemViewModel
    {
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        [Display(Name="Halle")]
        public int? Hall { get; set; }
        [Required]
        [Display(Name="Gang")]
        public int? Corridor { get; set; }
        [Required]
        [Display(Name="Regal")]
        public int? Rack { get; set; }
        [Required]
        [Display(Name="Ebene")]
        public int? Level { get; set; }
        [Required]
        [Display(Name="Position")]
        public int? Position { get; set; }

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
