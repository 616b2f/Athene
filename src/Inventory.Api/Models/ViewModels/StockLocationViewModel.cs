using System.Collections.Generic;

namespace Athene.Inventory.Web.ViewModels
{
    public class StockLocationViewModel
    {
        public int Id { get; set; }

        public int Hall { get; set; }
        public int Corridor { get; set; }
        public int Rack { get; set; }
        public int Level { get; set; }
        public int Position { get; set; }

        public string OneLiner { get; set; }
    }
}
