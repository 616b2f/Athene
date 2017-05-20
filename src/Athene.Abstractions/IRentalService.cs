using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions
{
    public interface IRentalService
    {
        void RentInventoryItem(string userId, int[] inventoryItemIds);
        IEnumerable<InventoryItem> FindRentedItemsByUser(string userId);
    }
}
