using System.Collections.Generic;
using Athene.Abstractions.Models;

namespace Athene.Abstractions.Utils
{
    public class MatchcodeGenerator
    {
        public static IEnumerable<string> CreateFor(Book book)
        {
            var list = new List<string>();
            list.Add(book.InternationalStandardBookNumber);
            list.Add(book.Description);
            list.Add(book.Name);
            list.Add(book.Title);
            list.Add(book.SubTitle);
            return list;
        }
    }
}