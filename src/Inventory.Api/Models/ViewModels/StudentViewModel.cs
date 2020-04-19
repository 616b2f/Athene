using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athene.Inventory.Web.ViewModels
{
    public class StudentViewModel
    {
        public string StudentId { get; set; }
        public int SchoolClassId { get; set; }
        public SchoolClassViewModel SchoolClass { get; set; }
    }
}
