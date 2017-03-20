using System;

namespace Athene.Inventory.Web.Models
{
    public class BoardOfEducation
    {
        private BoardOfEducation() {}
        public BoardOfEducation(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name parameter is null");
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
