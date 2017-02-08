using System.Collections.Generic;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.RentViewModels
{
    public class RentedViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<BookItem> RentedBooks { get; set; }
    }
}
