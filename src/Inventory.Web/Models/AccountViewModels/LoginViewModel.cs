using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Benutzername")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [Display(Name = "Eingeloggt bleiben?")]
        public bool RememberMe { get; set; }
    }
}
