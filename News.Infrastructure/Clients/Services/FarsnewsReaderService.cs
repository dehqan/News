using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using News.Core.Helper;
using News.Core.Models;
using News.Core.Models.Enum;
using News.Core.Services.Interfaces;
using News.Infrastructure.Clients.Models.Farsnews;

namespace News.Infrastructure.Clients.Services
{
    public class FarsnewsReaderService : IReaderService
    {
        private readonly IApiService _apiService;
        private readonly ILogger<FarsnewsReaderService> _logger;

        public FarsnewsReaderService(IApiService apiService, ILogger<FarsnewsReaderService> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<List<RssResult>> GetRssData(string url)
        {
            var data = await _apiService.SendRequest<FarsnewsRssResult>(ApiMethodEnum.GET, ApiSerializerEnum.XML, url);
            return data.Channel.Item.Select(x => new RssResult
            {
                Guid = x.Guid.Trim(),
                Id = long.Parse(x.Guid.Split('/').Last()),
                Link = x.Link.Trim(),
                Title = x.Title.Trim(),
                Description = x.Description.Trim(),
                PubDate = Convert.ToDateTime(x.PubDate)
            }).ToList();
        }

        public async Task<List<ReaderResult>> GetRssDetails(List<RssResult> dataList)
        {
            var readerResultList = new List<ReaderResult>();

            foreach (var rssResult in dataList)
            {
                try
                {
                    var data = await _apiService.SendRequest<string>(ApiMethodEnum.GET, ApiSerializerEnum.None, rssResult.Link);

                    var doc = new HtmlDocument();
                    doc.LoadHtml(data);

                    var date = Convert.ToDateTime(rssResult.PubDate);
                    var newsData = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'news-data')]");

                    var lead = newsData.SelectSingleNode("//p[contains(@class, 'lead')]")?.InnerHtml?.Trim();
                    var image = newsData.SelectSingleNode("//img")?.Attributes["src"].Value.Trim();
                    var body = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'nt-body')]");

                    body.RemoveAttributes(new List<string> { "class", "id", "style" });
                    
                    body.RemoveUnwantedHtmlTags(new List<string> { "a" });

                    readerResultList.Add(new ReaderResult
                    {
                        Id = rssResult.Id,
                        Title = rssResult.Title,
                        Lead = lead,
                        Image = image,
                        Body = body.InnerHtml.Trim(),
                        PublishDateTime = date,
                        Link = rssResult.Link
                    });
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, $"Client: Farsnews, Link: {rssResult.Link}");
                    continue;
                }
            }

            return readerResultList;
        }
    }
}
