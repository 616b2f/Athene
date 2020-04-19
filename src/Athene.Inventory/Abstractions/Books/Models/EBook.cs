using System.Collections.Generic;
using System.Linq;

namespace Athene.Inventory.Abstractions.Models
{
    public class EBook : Article
    {
        public EBook() 
        {
            this.Authors = new List<Author>();
            this.Categories = new List<Category>();
        }
        public override string Name { get { return Title + " " + SubTitle;} }

        public List<Author> Authors { get; private set; }
        public List<Category> Categories { get; private set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public override void GenerateMatchcodes()
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(this.Name))
                list.Add(this.Name);

            Matchcodes = list.Select(x => new Matchcode{Value=x.ToLower()}).ToList();
        }
    }
}