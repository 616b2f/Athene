using System;
using System.ComponentModel;

namespace Athene.Inventory.Web.Areas.Admin.Models
{
    public class StudentViewModel 
    {
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [DisplayName("Lastname")]
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public DateTime Birthsday { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string StudentId { get; set; }
        public string SchoolClass { get; set; }
        public string SchoolName { get; set; }
        public string AddressStreet { get;set; }
        public string AddressZip { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
    }
}