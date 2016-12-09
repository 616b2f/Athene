using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Athene.Inventory.Web.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthsday { get; set; }
    }
}
