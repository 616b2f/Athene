using System.Collections.Generic;

namespace Athene.Inventory.Abstractions.Models
{
    public abstract class Article 
    {
        public abstract int Id { get; set;}
        public abstract string  Name { get; }
        public string ImageUrl { get; set; }
        public abstract IEnumerable<string> Matchcodes { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}