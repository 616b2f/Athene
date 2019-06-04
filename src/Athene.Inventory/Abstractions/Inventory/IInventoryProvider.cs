using System;
using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions
{
    public interface IInventoryProvider
    {
        InventoryItem FindInventoryItemByBarcode<TType>(string barcode) where TType : Article;
        InventoryItem FindInventoryItemByExternalId<TType>(string externalId) where TType : Article;
        IEnumerable<InventoryItem> SearchByMatchcode<TType>(string matchcode) where TType : Article;
        InventoryItem FindInventoryItemById(int id);
        IEnumerable<InventoryItem> FindInventoryItemsById(int[] ids);
        IEnumerable<InventoryItem> FindInventoryItemsByArticleId(int[] ids);
        IEnumerable<InventoryItem> SearchByMatchcode(string matchcode);
        IEnumerable<StockLocation> SearchForLocations(int articleId);
        IEnumerable<InventoryItem> FindRentedItemsByUser(string userId);
    }
}