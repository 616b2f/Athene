using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.RentViewModels
{
    public class RentViewModel
    {
        public RentViewModel()
        {
        }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int[] BookItemIds { get; set; }
    }
}
