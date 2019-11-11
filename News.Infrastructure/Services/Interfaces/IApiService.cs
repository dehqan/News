using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Infrastructure.Enum;

namespace News.Infrastructure.Services.Interfaces
{
    public interface IApiService
    {
        Task<T> SendRequest<T>(ApiMethodEnum apiMethod, ApiSerializerEnum serializer, string url, Dictionary<string, string> headers = null, object body = null) where T : class;
    }
}
