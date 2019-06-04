namespace Athene.Inventory.Abstractions.Models
{
    public class Student
    {
        public string StudentId { get; set; }
        public int SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
    }
}
