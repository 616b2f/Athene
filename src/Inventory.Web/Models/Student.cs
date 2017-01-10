using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models
{
    public class Student
    {
        public Student() {
            this.RentedBooks = new HashSet<BookItem>();
        }
        public int StudentId { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string FullName
        {
            get
            {
                return this.Surname + " " + Lastname;
            }
        }

        [DataType(DataType.Date)]
        public DateTime Birthsday { get; set; }
        public ICollection<BookItem> RentedBooks { get; set; }
    }
}
