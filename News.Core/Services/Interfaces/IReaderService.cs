using System.Collections.Generic;
using System.Threading.Tasks;
using News.Core.Models;

namespace News.Core.Services.Interfaces
{
    public interface IReaderService
    {
        //Task<List<ReaderResult>> Read(string url);
        Task<List<RssResult>> GetRssData(string url);
        Task<List<ReaderResult>> GetRssDetails(List<RssResult> dataList);
    }
}
