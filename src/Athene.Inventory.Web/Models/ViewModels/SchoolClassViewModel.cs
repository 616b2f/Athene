using System;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.ViewModels
{
    public class SchoolClassViewModel
    {
        public int Id { get; private set; }
        [Display(Name="Klasse")]
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public SchoolViewModel School { get; set; }
    }
}
