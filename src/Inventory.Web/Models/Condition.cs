using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models
{
    public enum Condition
    {
        [Display(Name="Neu")]
        New = 0,
        [Display(Name="Wie neu")]
        LikeNew = 1,
        [Display(Name="Sehr gut")]
        VeryGood = 2,
        [Display(Name="Gut")]
        Good = 3,
        [Display(Name="Akzeptabel")]
        Acceptable = 4,
    }
}
