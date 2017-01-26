using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athene.Inventory.Web.Models
{
    public class BookItem
    {
        public BookItem()
        {
            this.Notes = new HashSet<BookItemNote>();
        }
        [Key]
        [DisplayFormat(DataFormatString = "{0:000000000}")]
        public int Id { get; set; }
        public Book Book { get; set; }
        [Display(Name="Standplatz")]
        public StockLocation StockLocation { get; set; }
        [Display(Name="Notizen")]
        public ICollection<BookItemNote> Notes { get; set; }
        public string RentedByUserId { get; set; }
        [Display(Name="Geliehen von")]
        [ForeignKey("RentedByUserId")]
        public ApplicationUser RentedBy { get; set; }
        [Display(Name="Geliehen am")]
        public DateTime? RentedAt { get;set; }
    }
}
