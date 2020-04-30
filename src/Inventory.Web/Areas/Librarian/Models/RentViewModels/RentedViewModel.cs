using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.RentDto
{
    public class RentedDto
    {
        public User User { get; set; }
        public IEnumerable<InventoryItem> RentedItems { get; set; }
    }
}
