using System.Collections.Generic;
using System.Linq;

namespace Athene.Inventory.Abstractions.Models
{
    public abstract class Article 
    {
        // private List<Matchcode> _matchcodes = new List<Matchcode>();
        public int ArticleId { get; set;}
        public abstract string  Name { get; }
        public string ImageUrl { get; set; }
        public IEnumerable<Matchcode> Matchcodes
        {
            get; protected set;
            // get
            // { 
            //     return _matchcodes; 
            // }
            // protected set
            // {
            //     _matchcodes = value.ToList();
            // }
        }

        public abstract void GenerateMatchcodes();
        
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}