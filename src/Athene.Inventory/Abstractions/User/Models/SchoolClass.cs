using System;

namespace Athene.Inventory.Abstractions.Models
{
    public class SchoolClass
    {
        private SchoolClass()
        {
        }

        public SchoolClass(string name, School school)
        {
            // if (school == null)
            //     throw new ArgumentNullException();
            Name = name;
            School = school;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
