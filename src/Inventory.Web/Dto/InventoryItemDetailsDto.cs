using System;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Web.Dto
{
    public class InventoryItemDetailsDto
    {
        public int InventoryItemId { get; set; }
        public string ExternalId { get; set; }
        public string Barcode { get; set; }
        public string StockLocation { get; set; }
        public string RentedByUserId { get; set; }
        public string RentedByUserDisplayName { get; set; }
        public DateTime? RentedAt { get; set; }
        public DateTime? PurchasedAt { get; set; }
        public Condition Condition { get; set; }
    }
}