using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Athene.Inventory.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() {
            RentedBooks = new HashSet<BookItem>();
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

        [DataType(DataType.Date)]
        public DateTime Birthsday { get; set; }
        public ICollection<BookItem> RentedBooks { get; set; }
    }
}
