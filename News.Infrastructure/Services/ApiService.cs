﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using News.Infrastructure.Enum;
using News.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace News.Infrastructure.Services
{
    public class ApiService : IApiService
    {
        public async Task<T> SendRequest<T>(ApiMethodEnum apiMethod, ApiSerializerEnum serializer, string url, Dictionary<string, string> headers = null, object body = null) where T : class
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = apiMethod.ToString();

            if (headers != null)
            {
                foreach (var (key, value) in headers)
                {
                    httpWebRequest.Headers.Add(key, value);
                }
            }

            if (body != null)
            {
                await using var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                var json = JsonConvert.SerializeObject(body, Formatting.None);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }


            using var response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            var apiResult = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(apiResult)) return null;

            switch (serializer)
            {
                case ApiSerializerEnum.None:
                    return (T)Convert.ChangeType(apiResult, typeof(T)); ;
                case ApiSerializerEnum.Json:
                    return JsonConvert.DeserializeObject<T>(apiResult);
                case ApiSerializerEnum.XML:
                    return new Serializer().Deserialize<T>(apiResult);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
