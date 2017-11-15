using System;
using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IInventory
    {
        void AddInventoryItem(InventoryItem item);
        void AddInventoryItems(IEnumerable<InventoryItem> items);
        IEnumerable<InventoryItem> SearchByMatchcode(string matchcode);
        IEnumerable<InventoryItem> SearchByMatchcode<TType>(string matchcode) where TType : Article;
        InventoryItem FindInventoryItemByBarcode<TType>(string barcode) where TType : Article;
        InventoryItem FindInventoryItemById(int id);
        IEnumerable<InventoryItem> FindInventoryItemById(int[] ids);
        IEnumerable<StockLocation> SearchForLocations(Article article);
        void RentInventoryItem(string userId, int[] inventoryItemIds, DateTime rentedAt);
        IEnumerable<InventoryItem> FindRentedItemsByUser(string userId);
    }
}