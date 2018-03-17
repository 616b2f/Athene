using System;
using System.Collections.Generic;

namespace Athene.Inventory.Abstractions.Models
{
    public class Book : Article
    {
        public Book() 
        {
            this.Authors = new List<Author>();
            this.Categories = new List<Category>();
            this.Matchcodes = new List<string>();
        }
        public override int Id { get; set; }
        public override string Name { get { return Title + " " + SubTitle;} }
        public override IEnumerable<string> Matchcodes { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string InternationalStandardBookNumber { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime? PublishedAt { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Category> Categories { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}