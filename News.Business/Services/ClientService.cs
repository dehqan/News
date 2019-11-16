using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Business.Services.Interfaces;
using News.Core;
using News.Core.Models;
using News.Domain.Contracts.Repositories;
using News.Domain.Model.Entity;
using News.Infrastructure.EntityFramework;

namespace News.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IEnumerable<IReaderService> _readerServices;
        private readonly IClientRepository _clientRepository;
        private readonly NewsDbContext _context;

        public ClientService(IEnumerable<IReaderService> readerServices, IClientRepository clientRepository, NewsDbContext context)
        {
            _readerServices = readerServices;
            _clientRepository = clientRepository;
            _context = context;
        }

        public async Task Read()
        {
            var clientList = await _clientRepository.GetList();
            foreach (var client in clientList)
            {
                var readerService = _readerServices.FirstOrDefault(x => x.GetType().Name.Contains(client.Title));
                if (readerService == null) continue;

                foreach (var clientCategory in client.ClientCategoryCollection)
                {
                    var result = await readerService.GetRssData(clientCategory.Url);
                    var getDataList = new List<RssResult>();
                    foreach (var rssResult in result)
                    {
                        var story = await _context.Stories.FirstOrDefaultAsync(x => x.ExternalId == rssResult.Id && x.ClientId == client.Id);
                        if (story != null) continue;

                        getDataList.Add(rssResult);

                    }

                    var dataList = await readerService.GetRssDetails(getDataList);
                    var storyList = dataList.Select(readerResult => new Story
                        {
                            ExternalId = readerResult.Id,
                            Link = readerResult.Link,
                            Body = readerResult.Body,
                            ClientId = client.Id,
                            Image = readerResult.Image,
                            Lead = readerResult.Lead,
                            Thumbnail = readerResult.Thumbnail,
                            Title = readerResult.Title,
                            PublishDateTime = readerResult.PublishDateTime
                        }).ToList();

                    await _context.AddRangeAsync(storyList);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
