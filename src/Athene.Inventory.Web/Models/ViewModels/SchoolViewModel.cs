using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.ViewModels
{
    public class SchoolViewModel
    {
        private SchoolViewModel() {
            BookItems = new HashSet<BookItemViewModel>();
            SchoolClasses = new HashSet<SchoolClassViewModel>();
        }

        public int Id { get; set; }
        [Display(Name="Schule")]
        public string Name { get; set; }
        [Display(Name="Schulkürzel")]
        public string ShortName { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZip { get;  set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public int BoardOfEducationId { get; set; }
        public ICollection<BookItemViewModel> BookItems { get; set; }
        public ICollection<SchoolClassViewModel> SchoolClasses { get; set; }
    }
}
