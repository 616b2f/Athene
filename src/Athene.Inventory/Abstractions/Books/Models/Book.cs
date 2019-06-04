using System;
using System.Collections.Generic;
using System.Linq;

namespace Athene.Inventory.Abstractions.Models
{
    public class Book : Article
    {
        public Book() 
        {
            this.Authors = new List<Author>();
            this.Categories = new List<Category>();
        }
        public override string Name { get { return Title + " " + SubTitle;} }

        public override void GenerateMatchcodes()
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(this.InternationalStandardBookNumber))
                list.Add(this.InternationalStandardBookNumber);
            if (!string.IsNullOrWhiteSpace(this.Description))
                list.Add(this.Description);
            if (!string.IsNullOrWhiteSpace(this.Name))
                list.Add(this.Name);
            if (!string.IsNullOrWhiteSpace(this.Title))
                list.Add(this.Title);
            if (!string.IsNullOrWhiteSpace(this.SubTitle))
                list.Add(this.SubTitle);

            Matchcodes = list.Select(x => new Matchcode{Value=x.ToLower()}).ToList();
        }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string InternationalStandardBookNumber { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime? PublishedAt { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Category> Categories { get; set; }
        public int? LanguageId { get; set; }
        public Language Language { get; set; }
    }
}