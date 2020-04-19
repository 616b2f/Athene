using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace  Athene.Inventory.Web.ViewModels
{
    public class SearchResultViewModel
    {
        public int FoundQuantity { get; set; }
        public IEnumerable<Article> Items { get; set; }
    }
}