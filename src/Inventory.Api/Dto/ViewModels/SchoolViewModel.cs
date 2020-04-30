using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Dto
{
    public class SchoolDto
    {
        private SchoolDto() {
            BookItems = new HashSet<BookItemDto>();
            SchoolClasses = new HashSet<SchoolClassDto>();
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
        public ICollection<BookItemDto> BookItems { get; set; }
        public ICollection<SchoolClassDto> SchoolClasses { get; set; }
    }
}
