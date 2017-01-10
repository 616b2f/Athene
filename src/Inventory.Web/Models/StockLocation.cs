using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athene.Inventory.Web.Models
{
    public class StockLocation
    {
        public StockLocation()
        {
            this.BookItems = new HashSet<BookItem>();
        }

        [Key]
        [Column(Order = 0)]
        public int Hall { get; set; }
        [Key]
        [Column(Order = 1)]
        public int Corridor { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Rack { get; set; }
        [Key]
        [Column(Order = 3)]
        public int Level { get; set; }
        [Key]
        [Column(Order = 4)]
        public int Position { get; set; }

        public string OneLiner
        {
            get
            {
                return this.Hall + "/" +
                    this.Corridor + "/" +
                    this.Rack + "/" +
                    this.Level + "/" +
                    this.Position;
            }
        }
        public ICollection<BookItem> BookItems { get; set; }
    }
}
