using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using News.Business.Clients.Models;
using News.Business.Clients.Models.Tasnimnews;
using News.Core;
using News.Core.Models;
using News.Core.Models.Enum;
using News.Core.Services.Interfaces;

namespace News.Business.Clients.Services
{
    public class TasnimnewsReaderService : IReaderService
    {
        private readonly IApiService _apiService;
        public TasnimnewsReaderService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<ReaderResult>> Read(string url)
        {
            var result = await _apiService.SendRequest<TasnimnewsRssResult>(ApiMethodEnum.GET, ApiSerializerEnum.XML, url);
            var readerResultList = new List<ReaderResult>();

            foreach (var item in result.Channel.Item)
            {
                try
                {
                    var data = await _apiService.SendRequest<string>(ApiMethodEnum.GET, ApiSerializerEnum.None, item.Guid);

                    var doc = new HtmlDocument();
                    doc.LoadHtml(data);

                    var date = Convert.ToDateTime(item.PubDate);
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
                catch (Exception e)
                {
                    continue;
                }
            }

            return readerResultList;
        }


    }
}
