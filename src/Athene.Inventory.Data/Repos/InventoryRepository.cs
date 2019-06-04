using System;
using System.Collections.Generic;
using System.Linq;
using Athene.Inventory.Abstractions;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Athene.Inventory.Data.Services
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _db;

        public InventoryRepository(InventoryDbContext dbContext)
        {
            this._db = dbContext;
        }

        public void AddInventoryItem(InventoryItem item)
        {
            AddInventoryItems(new[] { item });
        }

        public void AddInventoryItems(IEnumerable<InventoryItem> items)
        {
            _db.AddRange(items);
        }

        public IEnumerable<InventoryItem> FindInventoryItemsById(int[] ids)
        {
            return _db.InventoryItems.Where(x => ids.Contains(x.Id)).AsEnumerable();
        }
    }
}