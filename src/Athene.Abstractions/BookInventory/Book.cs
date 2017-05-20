using System;
using System.Collections.Generic;

namespace Athene.Abstractions.Models
{
    public class Book : Article
    {
        public Book() 
        {
            this.Authors = new HashSet<Author>();
            this.InventoryItems = new HashSet<InventoryItem>();
            this.Categories = new HashSet<Category>();
        }
        public override int Id { get; set; }
        public override string Name { get { return Title + " " + SubTitle;} }
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
        public ICollection<InventoryItem> InventoryItems { get; set; }
    }
}