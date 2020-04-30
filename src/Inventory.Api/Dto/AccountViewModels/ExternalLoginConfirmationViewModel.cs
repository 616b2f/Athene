using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Athene.Inventory.Web.Models.AccountDto
{
    public class ExternalLoginConfirmationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
