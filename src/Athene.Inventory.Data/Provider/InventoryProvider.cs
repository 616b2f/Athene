using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Data.Services
{
    public class InventoryProvider : IInventoryProvider
    {
        private readonly InventoryDbContext _db;

        public InventoryProvider(InventoryDbContext dbContext)
        {
            this._db = dbContext;
        }

        public InventoryItem FindInventoryItemByBarcode<TType>(string barcode) where TType : Article 
        {
            return _db.InventoryItems.SingleOrDefault(x => 
                    x.Barcode == barcode &&
                    x.Article is TType);
        }

        public InventoryItem FindInventoryItemByExternalId<TType>(string externalId) where TType : Article 
        {
            return _db.InventoryItems.SingleOrDefault(x => 
                    x.ExternalId == externalId &&
                    x.Article is TType);
        }

        public IEnumerable<InventoryItem> SearchByMatchcode<TType>(string matchcode) where TType : Article
        {
            return _db.InventoryItems
                .Where(x => 
                    (x.Id.ToString() == matchcode ||
                     x.Barcode == matchcode ||
                     x.Article.Name.Contains(matchcode)) &&
                     x.Article is TType);
        }

        public InventoryItem FindInventoryItemById(int id)
        {
            return _db.InventoryItems.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<InventoryItem> FindInventoryItemsById(int[] ids)
        {
            return _db.InventoryItems.Where(x => ids.Contains(x.Id)).AsEnumerable();
        }

        public IEnumerable<InventoryItem> FindInventoryItemsByArticleId(int[] ids)
        {
            return _db.InventoryItems
                .Include(x => x.StockLocation)
                .Include(x => x.Article)
                .Where(x => ids.Contains(x.Article.ArticleId))
                .ToList();
        }

        public IEnumerable<InventoryItem> SearchByMatchcode(string matchcode)
        {
            return _db.InventoryItems.Where(x => 
                x.Id.ToString() == matchcode ||
                x.Barcode == matchcode ||
                x.Article.Name.Contains(matchcode));
        }

        public IEnumerable<StockLocation> SearchForLocations(int articleId)
        {
            return _db.InventoryItems
                .Where(i => i.Article.ArticleId == articleId)
                .Select(i => i.StockLocation)
                .Distinct();
        }
        public IEnumerable<InventoryItem> FindRentedItemsByUser(string userId)
        {
            return _db.InventoryItems.Where(i => i.RentedByUserId == userId);
        }
    }
}