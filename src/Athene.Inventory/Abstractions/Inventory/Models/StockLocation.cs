using System.Collections.Generic;

namespace Athene.Inventory.Abstractions.Models
{
    public class StockLocation
    {
        public StockLocation()
        {
            this.InventoryItems = new HashSet<InventoryItem>();
        }

        public int Id { get; set; }

        public string Hall { get; set; }
        public string Corridor { get; set; }
        public string Rack { get; set; }
        public string Level { get; set; }
        public string Position { get; set; }

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
        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
