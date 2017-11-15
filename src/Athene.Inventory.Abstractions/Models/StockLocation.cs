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

        public int Hall { get; set; }
        public int Corridor { get; set; }
        public int Rack { get; set; }
        public int Level { get; set; }
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
        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
