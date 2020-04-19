using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Athene.Inventory.Abstractions.Models;
using Microsoft.AspNetCore.Identity;

namespace Athene.Inventory
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public User() {
            RentedItems = new HashSet<InventoryItem>();
        }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string FullName
        {
            get
            {
                return this.Surname + " " + Lastname;
            }
        }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthsday { get; set; }
        public ICollection<InventoryItem> RentedItems { get; set; }
        [Display(Name="Schüler Nr.")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string MobilePhoneNumber { get; set; }
        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; } = new List<IdentityUserLogin<string>>();
    }
}
