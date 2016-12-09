using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Athene.Inventory.Web.Models
{
    public class BookItemNote
    {
        public BookItemNote()
        {
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}
