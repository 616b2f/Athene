using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Models
{
    public class Book
    {
        public Book() 
        {
            this.Authors = new HashSet<Author>();
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string InternationalStandardBookNumber10 { get; set; }
        public string InternationalStandardBookNumber13 { get; set; }
        public string EuropeanArticleNumber { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Author> Authors { get; set; }
        [Required]
        public Language Language { get; set; }
    }
}
