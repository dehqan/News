using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using News.Clients.Tasnimnews.Models;
using News.Core;
using News.Core.Models;
using News.Core.Services.Interfaces;
using News.Domain.Enum;

namespace News.Clients.Tasnimnews
{
    public class Reader : IReader
    {
        private readonly IApiService _apiService;
        public Reader(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<ReaderResult>> Read(string url)
        {
            var result = await _apiService.SendRequest<RssResult>(ApiMethodEnum.GET, ApiSerializerEnum.XML, url);
            var readerResultList = new List<ReaderResult>();
            foreach (var item in result.Channel.Item)
            {
                var data = await _apiService.SendRequest<string>(ApiMethodEnum.GET, ApiSerializerEnum.None, item.Guid);

                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                var date = Convert.ToDateTime(item.PubDate);
                var newsData = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'news-data')]");
                var title = item.Title.Trim();
                var lead = item.Description.Trim();
                var image = item.Content.Url;
                var thumbnail = item.Thumbnail.Url;
                var body = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'story')]");
                foreach (var bodyChildNode in body.ChildNodes)
                {
                    bodyChildNode.Attributes.Remove("class");
                    bodyChildNode.Attributes.Remove("id");
                    bodyChildNode.Attributes.Remove("style");
                }

                var newBody = body.InnerHtml.RemoveUnwantedHtmlTags(new List<string> { "a" });

                readerResultList.Add(new ReaderResult
                {
                    Id = Convert.ToInt64(item.Guid.Split('/').Last()),
                    Title = title,
                    Lead = lead,
                    Thumbnail = thumbnail,
                    Image = image,
                    Body = newBody,
                    PublishDateTime = date,
                    Link = item.Guid
                });
            }

            return readerResultList;
        }


    }
}
