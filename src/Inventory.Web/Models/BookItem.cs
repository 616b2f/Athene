using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models
{
    public class BookItem
    {
        public BookItem()
        {
            this.Notes = new HashSet<BookItemNote>();
        }
        [Key]
        public int Id { get; set; }
        public Book Book { get; set; }
        [Display(Name="Standplatz")]
        public StockLocation StockLocation { get; set; }
        [Display(Name="Notizen")]
        public ICollection<BookItemNote> Notes { get; set; }

        [Display(Name="Geliehen von")]
        public Student RentedBy { get;set; }
        [Display(Name="Geliehen am")]
        public DateTime? RentedAt { get;set; }
    }
}
