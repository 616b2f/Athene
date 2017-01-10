using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Data;

namespace Athene.Inventory.Web.Services
{
    public class StudentRepository : IStudentsRepository
    {
        private readonly InventoryDbContext _db;
        public StudentRepository(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Student> Find(string matchcode)
        {
            var students = _db.Students
                .Where(s => 
                        s.Surname.StartsWith(matchcode) ||
                        s.Lastname.StartsWith(matchcode))
                .ToList();
            return students;
        }

        public void Add(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
        }
    }
}
