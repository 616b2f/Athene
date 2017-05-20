using System;
using System.Collections.Generic;
using Athene.Abstractions.Models;
using System.Linq;

namespace Athene.Abstractions
{
    public class InMemoryRentalService : IRentalService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInventory _inventory;
        private readonly Dictionary<string, List<int>> _rentTable = new Dictionary<string, List<int>>();
        public InMemoryRentalService(
            IUserRepository userRepository,
            IInventory inventory)
        {
            _userRepository = userRepository;
            _inventory = inventory;
        }

        public IEnumerable<InventoryItem> FindRentedItemsByUser(string userId)
        {
            if (_rentTable.ContainsKey(userId))
            {
                var inventoryIds = _rentTable[userId].ToArray();
                return _inventory.FindInventoryItemById(inventoryIds);
            }
            else
                return Enumerable.Empty<InventoryItem>();
        }

        public void RentInventoryItem(string userId, int[] inventoryItemIds)
        {
            throw new NotImplementedException();
        }
    }
}