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
using News.Infrastructure.Clients.Models.Mehrnews;
using News.Infrastructure.Clients.Models.Tasnimnews;

namespace News.Infrastructure.Clients.Services
{
    public class MehrnewsReaderService : IReaderService
    {
        private readonly IApiService _apiService;
        private readonly ILogger<MehrnewsReaderService> _logger;

        public MehrnewsReaderService(IApiService apiService, ILogger<MehrnewsReaderService> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<List<RssResult>> GetRssData(string url)
        {
            var data = await _apiService.SendRequest<MehrnewsRssResult>(ApiMethodEnum.GET, ApiSerializerEnum.XML, url);
            return data.Channel.Item.Select(x => new RssResult
            {
                Guid = x.Guid.Trim(),
                Id = long.Parse(x.Link.Split('/')[x.Link.Split('/').Length - 2]),
                Link = x.Link.Trim(),
                Title = x.Title.Trim(),
                Description = x.Description.Trim(),
                PubDate = Convert.ToDateTime(x.PubDate),
                Image = x.Enclosure.Url.Trim(),
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

                    var body = doc.DocumentNode.SelectSingleNode("//div[contains(@itemprop, 'articleBody')]");

                    body.RemoveAttributes(new List<string> { "class", "id", "style" });
                    body.RemoveUnwantedHtmlTags(new List<string> { "a" });

                    readerResultList.Add(new ReaderResult
                    {
                        Id = rssResult.Id,
                        Title = rssResult.Title,
                        Lead = rssResult.Description,
                        Image = rssResult.Image,
                        Body = body.InnerHtml.Trim(),
                        PublishDateTime = rssResult.PubDate,
                        Link = rssResult.Link
                    });
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, $"Client: Mehrnews, Link: {rssResult.Link}");
                    continue;
                }
            }

            return readerResultList;
        }


    }
}
