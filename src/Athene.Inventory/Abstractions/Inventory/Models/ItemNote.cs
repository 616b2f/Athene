using System;
using System.Collections.Generic;
using System.Linq;

namespace Athene.Inventory.Abstractions.Models
{
    public class ItemNote
    {
        public ItemNote()
        {
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}
