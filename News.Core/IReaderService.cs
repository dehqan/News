using System.Collections.Generic;
using System.Threading.Tasks;
using News.Core.Models;

namespace News.Core
{
    public interface IReaderService
    {
        Task<List<ReaderResult>> Read(string url);
    }
}
