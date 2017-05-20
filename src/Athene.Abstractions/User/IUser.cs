using System;
using System.Collections.Generic;

namespace Athene.Abstractions.Models
{
    public interface IUser
    {
        string Id { get; set; }
        string Surname { get; set; }
        string Lastname { get; set; }
        string FullName { get; }
        Gender Gender { get; set; }
        Address Address { get; set; }
        DateTime Birthsday { get; set; }
        ICollection<InventoryItem> RentedItems { get; set; }
        string StudentId { get; set; }
        Student Student { get; set; }
    }
}
