using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions.TestImp
{
    public class InMemoryInventory : IInventory
    {
        private readonly List<InventoryItem> _inventoryItems;

        public InMemoryInventory()
        {
            _inventoryItems = new List<InventoryItem>();
        }

        public void AddInventoryItem(InventoryItem item)
        {
            if (item.Id == 0)
            {
                int maxId = _inventoryItems.Max(x => x.Id);
                item.Id = maxId + 1;
            }
            _inventoryItems.Add(item);
        }

        public void AddInventoryItems(IEnumerable<InventoryItem> items)
        {
            foreach (var item in items)
            {
                AddInventoryItem(item);
            }
        }

        public InventoryItem FindInventoryItemByBarcode<TType>(string barcode) where TType  : Article 
        {
            return _inventoryItems.SingleOrDefault(x => 
                    x.Barcode == barcode &&
                    x.Article is TType);
        }

        public InventoryItem FindInventoryItemByExternalId<TType>(string externalId) where TType  : Article 
        {
            return _inventoryItems.SingleOrDefault(x => 
                    x.ExternalId == externalId &&
                    x.Article is TType);
        }

        public IEnumerable<InventoryItem> SearchByMatchcode<TType>(string matchcode) where TType : Article
        {
            return _inventoryItems
                .Where(x => 
                    (x.Id.ToString() == matchcode ||
                     x.Barcode == matchcode ||
                     x.Article.Name.Contains(matchcode)) &&
                     x.Article is TType);
        }

        public InventoryItem FindInventoryItemById(int id)
        {
            return _inventoryItems.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<InventoryItem> FindInventoryItemsById(int[] ids)
        {
            return _inventoryItems.Where(x => ids.Contains(x.Id)).AsEnumerable();
        }

        public IEnumerable<InventoryItem> FindInventoryItemsByArticleId(int[] ids)
        {
            return _inventoryItems.Where(x => ids.Contains(x.Article.Id)).AsEnumerable();
        }

        public IEnumerable<InventoryItem> SearchByMatchcode(string matchcode)
        {
            return _inventoryItems.Where(x => 
                x.Id.ToString() == matchcode ||
                x.Barcode == matchcode ||
                x.Article.Name.Contains(matchcode));
        }

        public IEnumerable<StockLocation> SearchForLocations(Article article)
        {
            return _inventoryItems
                .Where(i => i.Article.Id == article.Id)
                .Select(i => i.StockLocation)
                .Distinct();
        }
        public IEnumerable<InventoryItem> FindRentedItemsByUser(string userId)
        {
            return _inventoryItems.Where(i => i.RentedByUserId == userId);
        }

        public void RentInventoryItem(string userId, int[] inventoryItemIds, DateTime rentedAt)
        {
            var items = FindInventoryItemsById(inventoryItemIds);
            foreach (var item in items)
            {
                item.RentedByUserId = userId;
                item.RentedAt = rentedAt;
            }
        }
    }
}