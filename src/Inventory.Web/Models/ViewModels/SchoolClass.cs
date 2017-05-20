using System;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models
{
    public class SchoolClass
    {
        private SchoolClass()
        {
        }

        public SchoolClass(string name, School school)
        {
            if (school == null)
                throw new ArgumentNullException();
            Name = name;
            School = school;
        }

        [Key]
        public int Id { get; private set; }
        [Display(Name="Klasse")]
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
