using System.IO;
using System.Collections.Generic;

namespace Athene.Abstractions.DataImport
{
    public interface IDataImport<T>
    {
        IEnumerable<T> Convert(Stream fileStream);
    }
}