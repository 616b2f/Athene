using System;

namespace Athene.Inventory.Web.Dto
{
    public class BoardOfEducationDto
    {
        private BoardOfEducationDto() {}
        public BoardOfEducationDto(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name parameter is null");
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
