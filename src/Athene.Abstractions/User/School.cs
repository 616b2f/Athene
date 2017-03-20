using System;
using System.Collections.Generic;

namespace Athene.Abstractions.Models
{
    public class School
    {
        private School() {
            InventoryItems = new HashSet<InventoryItem>();
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
            InventoryItems = new HashSet<InventoryItem>();
            SchoolClasses = new HashSet<SchoolClass>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Address Address { get; set; }
        public int BoardOfEducationId { get; set; }
        public BoardOfEducation BoardOfEducation { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }
        public ICollection<SchoolClass> SchoolClasses { get; set; }
    }
}
