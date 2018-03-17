using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;

namespace Athene.Inventory.Abstractions.Utils
{
    public class MatchcodeGenerator
    {
        public static IEnumerable<string> CreateFor(Book book)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(book.InternationalStandardBookNumber))
                list.Add(book.InternationalStandardBookNumber);
            if (!string.IsNullOrWhiteSpace(book.Description))
                list.Add(book.Description);
            if (!string.IsNullOrWhiteSpace(book.Name))
                list.Add(book.Name);
            if (!string.IsNullOrWhiteSpace(book.Title))
                list.Add(book.Title);
            if (!string.IsNullOrWhiteSpace(book.SubTitle))
                list.Add(book.SubTitle);
            return list;
        }
    }
}