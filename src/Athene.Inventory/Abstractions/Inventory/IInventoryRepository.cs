using System;
using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IInventoryRepository
    {
        void AddInventoryItem(InventoryItem item);
        void AddInventoryItems(IEnumerable<InventoryItem> items);
        IEnumerable<InventoryItem> FindInventoryItemsById(int[] ids);
        InventoryItem FindInventoryItemById(int id);
    }
}
