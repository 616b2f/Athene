using System;
using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions.TestImp
{
    public class TestUser : IUser
    {
        public string Id { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }

        public string FullName 
        { 
            get { return Surname + " " + Lastname; }
        }

        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public DateTime Birthsday { get; set; }
        public ICollection<InventoryItem> RentedItems { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
    }
}