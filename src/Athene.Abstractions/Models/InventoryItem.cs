using System;
using System.Collections.Generic;

namespace Athene.Abstractions.Models
{
    public class InventoryItem
    {
        public InventoryItem()
        {
            this.Notes = new HashSet<ItemNote>();
        }
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Barcode { get; set; }
        public Article Article { get; set; }
        public StockLocation StockLocation { get; set; }
        public ICollection<ItemNote> Notes { get; set; }
        public string RentedByUserId { get; set; }
        public IUser RentedBy { get; set; }
        public DateTime? RentedAt { get; set; }
        public DateTime? PurchasedAt { get; set; }
        public decimal PurchasePrice { get; set; }
        public Condition Condition { get; set; }
    }
}
