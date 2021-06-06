using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Extensions
{
    public enum UserMessageType
    {
        [Display(Name = "Erfolgreich")]
        Success,
        [Display(Name = "Warnung")]
        Warning,
        [Display(Name = "Fehler")]
        Error,
        [Display(Name = "Info")]
        Info
    }
}
