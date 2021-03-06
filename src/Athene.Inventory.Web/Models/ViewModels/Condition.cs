using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.ViewModels
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
        [Display(Name="Schlecht")]
        Bad = 5,
    }
}
