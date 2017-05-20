using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models
{
    public class School
    {
        private School() {
            BookItems = new HashSet<BookItem>();
        }
        public School(string name, string shortName, Address address,
                BoardOfEducation boardOfEducation)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name parameter is null");
            if (address == null)
                throw new ArgumentNullException("address parameter is null");
            if (boardOfEducation == null)
                throw new ArgumentNullException("boardOfEducation parameter is null");
            Name = name;
            ShortName = shortName;
            Address = address;
            BoardOfEducation = boardOfEducation;
            BookItems = new HashSet<BookItem>();
            SchoolClasses = new HashSet<SchoolClass>();
        }

        [Key]
        public int Id { get; set; }
        [Display(Name="Schule")]
        public string Name { get; set; }
        [Display(Name="Schulkürzel")]
        public string ShortName { get; set; }
        public Address Address { get; set; }
        public int BoardOfEducationId { get; set; }
        public BoardOfEducation BoardOfEducation { get; set; }
        public ICollection<BookItem> BookItems { get; set; }
        public ICollection<SchoolClass> SchoolClasses { get; set; }
    }
}
