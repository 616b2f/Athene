using System.Collections.Generic;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web
{
    public interface IStudentsRepository
    {
        IEnumerable<ApplicationUser> Find(string matchcode);
        ApplicationUser FindByUserId(string userId);
        void Add(ApplicationUser user);
    }
}
