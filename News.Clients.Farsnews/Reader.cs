using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using News.Clients.Farsnews.Models;
using News.Infrastructure.Enum;
using News.Infrastructure.Services;
using News.Infrastructure.Services.Interfaces;

namespace News.Clients.Farsnews
{
    public class Reader : IReader
    {
        private readonly IApiService _apiService;
        public Reader(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task Read()
        {
            var result = await _apiService.SendRequest<RssResult>(ApiMethodEnum.GET, ApiSerializerEnum.XML, "https://www.farsnews.com/rss/economy");

            foreach (var item in result.Channel.Item)
            {
                var data = await _apiService.SendRequest<string>(ApiMethodEnum.GET, ApiSerializerEnum.None, item.Link);

                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                var x = doc.DocumentNode.Descendants("div").ToList().FirstOrDefault(d => d.Attributes["class"].Value.Contains("headline"));

            }
        }
    }
}
