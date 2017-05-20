using System;
using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions
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
    }
}