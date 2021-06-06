using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Web.Dto
{
   public class EditBookDto
   {
        public EditBookDto() {
            this.Authors = new HashSet<AuthorDto>();
            this.Categories = new HashSet<CategoryDto>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
        public string InternationalStandardBookNumber { get; set; }
        public ICollection<AuthorDto> Authors { get; set; }
        [Required]
        public int PublisherId { get; set; }
        public PublisherDto Publisher { get; set; }
        public DateTime? PublishedAt { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public int[] AuthorsIds { get; set; }
        [Required]
        public int[] CategoriesIds { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
        public string Language { get; set; }
   } 
}