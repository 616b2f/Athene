using System.Collections.Generic;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> Find(string matchcode);
        void Add(Student student);
    }
}
