using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using News.Business.Clients.Models;
using News.Business.Clients.Models.Farsnews;
using News.Core;
using News.Core.Models;
using News.Core.Models.Enum;
using News.Core.Services.Interfaces;

namespace News.Business.Clients.Services
{
    public class FarsnewsReaderService : IReaderService
    {
        private readonly IApiService _apiService;
        public FarsnewsReaderService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<ReaderResult>> Read(string url)
        {
            var result = await _apiService.SendRequest<FarsnewsRssResult>(ApiMethodEnum.GET, ApiSerializerEnum.XML, url);
            var readerResultList = new List<ReaderResult>();

            foreach (var item in result.Channel.Item)
            {
                try
                {
                    var data = await _apiService.SendRequest<string>(ApiMethodEnum.GET, ApiSerializerEnum.None, item.Guid);

                    var doc = new HtmlDocument();
                    doc.LoadHtml(data);

                    var date = Convert.ToDateTime(item.PubDate);
                    var newsData = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'news-data')]");
                    if (newsData == null) continue; // skip some news

                    var title = item.Title.Trim();
                    var lead = newsData.SelectSingleNode("//p[contains(@class, 'lead')]")?.InnerHtml?.Trim();
                    var image = newsData.SelectSingleNode("//img")?.Attributes["src"].Value;
                    var body = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'nt-body')]");
                    foreach (var bodyChildNode in body.ChildNodes)
                    {
                        bodyChildNode.Attributes.Remove("class");
                        bodyChildNode.Attributes.Remove("id");
                        bodyChildNode.Attributes.Remove("style");
                    }

                    var newBody = body.InnerHtml.RemoveUnwantedHtmlTags(new List<string> { "a" }).Trim();

                    readerResultList.Add(new ReaderResult
                    {
                        Id = Convert.ToInt64(item.Guid.Split('/').Last()),
                        Title = title,
                        Lead = lead,
                        Image = image,
                        Body = newBody,
                        PublishDateTime = date,
                        Link = item.Guid
                    });
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            return readerResultList;
        }


    }
}
