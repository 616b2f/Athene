using System.Collections.Generic;

namespace Athene.Abstractions.Models
{
    public abstract class Article 
    {
        public abstract int Id { get; set;}
        public abstract string  Name { get; }
        public abstract IEnumerable<string> Matchcodes { get; }
    }
}