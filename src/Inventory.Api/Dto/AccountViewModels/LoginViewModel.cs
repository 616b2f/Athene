using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models.AccountDto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Username")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Keep me logged in?")]
        public bool RememberMe { get; set; }
    }
}
