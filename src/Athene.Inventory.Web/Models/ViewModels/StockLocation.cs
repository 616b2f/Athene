using System.Collections.Generic;

namespace Athene.Inventory.Web.ViewModels
{
    public class StockLocation
    {
        public StockLocation()
        {
            this.BookItems = new HashSet<BookItemViewModel>();
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
        public ICollection<BookItemViewModel> BookItems { get; set; }
    }
}