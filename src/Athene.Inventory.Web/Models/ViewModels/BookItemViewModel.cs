using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Web.ViewModels
{
    public class BookItemViewModel
    {
        public BookItemViewModel()
        {
            this.Notes = new HashSet<ItemNote>();
        }
        [DisplayFormat(DataFormatString = "{0:000000000}")]
        public int Id { get; set; }
        public Book Book { get; set; }
        [Display(Name="Standplatz")]
        public StockLocationViewModel StockLocation { get; set; }
        [Display(Name="Notizen")]
        public ICollection<ItemNote> Notes { get; set; }
        public string RentedByUserId { get; set; }
        [Display(Name="Geliehen von")]
        public string RentedByName { get; set; }
        [Display(Name="Geliehen am")]
        public DateTime? RentedAt { get; set; }
        [Display(Name="Kaufdatum")]
        public DateTime PurchasedAt { get; set; }
        [Display(Name="Zustand")]
        public Condition Condition { get; set; }
    }
}
