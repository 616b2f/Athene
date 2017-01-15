using System.Collections.Generic;
using Athene.Inventory.Web.Models;

namespace Athene.Inventory.Web
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> Find(string matchcode);
        Student Find(int studentId);
        void Add(Student student);
    }
}
