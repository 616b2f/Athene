using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.RentDto
{
    public class RentDto
    {
        public RentDto()
        {
        }
        public string UserId { get; set; }
        public User User { get; set; }
        public int[] BookItemIds { get; set; }
    }
}
