using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Athene.Inventory.Web.Dto
{
    public class ItemNoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}
