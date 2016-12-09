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
        public StockLocation StockLocation { get; set; }
        public ICollection<BookItemNote> Notes { get; set; }
    }
}
