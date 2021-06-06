using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Athene.Inventory.Web.Dto
{
    public class StudentDto
    {
        public string StudentId { get; set; }
        public int SchoolClassId { get; set; }
        public SchoolClassDto SchoolClass { get; set; }
    }
}
