using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Web.Dto
{
    public class CreateBookDto
    {
        public CreateBookDto() {
            this.Authors = new HashSet<Author>();
            this.Categories = new HashSet<Category>();
        }
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
        public string InternationalStandardBookNumber { get; set; }
        public ICollection<Author> Authors { get; set; }
        [Required]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime? PublishedAt { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public int[] AuthorsIds { get; set; }
        [Required]
        public int[] CategoriesIds { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Language Language { get; set; }
    }
}
