using System.IO;
using System.Collections.Generic;

namespace Athene.Inventory.Abstractions.DataImport
{
    public interface IDataImport
    {
        string InputFormat { get; }
        string OutputFormat { get; }
        IEnumerable<TClass> Convert<TClass>(Stream fileStream, string delimiter = ";")
					where TClass : class;
    }
}
