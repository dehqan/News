using System.Collections.Generic;
using System.Threading.Tasks;
using News.Core.Models.Enum;

namespace News.Core.Services.Interfaces
{
    public interface IApiService
    {
        Task<T> SendRequest<T>(ApiMethodEnum apiMethod, ApiSerializerEnum serializer, string url, Dictionary<string, string> headers = null, object body = null) where T : class;
    }
}
