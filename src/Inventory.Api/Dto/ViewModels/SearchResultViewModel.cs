using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace  Athene.Inventory.Web.Dto
{
    public class SearchResultDto
    {
        public int FoundQuantity { get; set; }
        public IEnumerable<Article> Items { get; set; }
    }
}