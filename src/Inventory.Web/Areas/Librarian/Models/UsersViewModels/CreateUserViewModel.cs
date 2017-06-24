using System;
using System.ComponentModel.DataAnnotations;
using Athene.Abstractions.Models;

namespace Athene.Inventory.Web.Areas.Librarian.Models.UsersViewModels
{
    public class CreateUserViewModel
    {
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZip { get;  set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthsday { get; set; }
        [Display(Name="Schüler Nr.")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public int SchoolId { get; set; }
        public int SchoolClassId { get; set; }
        public int SchoolClassName { get; set; }
    }
}