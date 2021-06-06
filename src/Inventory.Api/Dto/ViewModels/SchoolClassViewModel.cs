using System;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Dto
{
    public class SchoolClassDto
    {
        public int Id { get; private set; }
        [Display(Name="Klasse")]
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public SchoolDto School { get; set; }
    }
}
