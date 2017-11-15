using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.EntityFramework
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    // public class ApplicationUser : IdentityUser, IUser
    // {
    //     public ApplicationUser() {
    //         InventoryItems = new HashSet<InventoryItem>();
    //     }
    //     public string Surname { get; set; }
    //     public string Lastname { get; set; }
    //     public string FullName
    //     {
    //         get
    //         {
    //             return this.Surname + " " + Lastname;
    //         }
    //     }
    //     public Gender Gender { get; set; }
    //     public Address Address { get; set; }
    //     [DataType(DataType.Date)]
    //     public DateTime Birthsday { get; set; }
    //     public ICollection<InventoryItem> InventoryItems { get; set; }
    //     public string StudentId { get; set; }
    //     public Student Student { get; set; }
    // }
}
