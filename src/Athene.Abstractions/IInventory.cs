using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions
{
    public interface IInventory
    {
        void AddInventoryItem(InventoryItem item);
        IEnumerable<InventoryItem> SearchByMatchcode(string matchcode);
        InventoryItem FindInventoryItemByBarcode(string barcode);
        InventoryItem FindInventoryItemById(int id);
        IEnumerable<InventoryItem> FindInventoryItemById(int[] ids);
    }
}
