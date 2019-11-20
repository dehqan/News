using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using News.Business.Services.Interfaces;
using News.Core;
using News.Core.Models;
using News.Core.Services.Interfaces;
using News.Domain.Contracts.Repositories;
using News.Domain.Model.Entity;
using News.Infrastructure.EntityFramework;

namespace News.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IEnumerable<IReaderService> _readerServices;
        private readonly IClientRepository _clientRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IEnumerable<IReaderService> readerServices, IClientRepository clientRepository, IStoryRepository storyRepository, ILogger<ClientService> logger)
        {
            _readerServices = readerServices;
            _clientRepository = clientRepository;
            _storyRepository = storyRepository;
            _logger = logger;
        }

        public async Task Read()
        {
            var clientList = await _clientRepository.GetList();
            foreach (var client in clientList)
            {
                _logger.LogInformation($"Client: {client.Title}");
                var readerService = _readerServices.FirstOrDefault(x => x.GetType().Name.ToLower().Contains(client.Title.ToLower()));
                if (readerService == null) continue;

                foreach (var clientCategory in client.ClientCategoryCollection)
                {
                    _logger.LogInformation($"Client: {client.Title}, ClientCategory: {clientCategory.Id}");

                    List<RssResult> result;
                    try
                    {
                        result = await readerService.GetRssData(clientCategory.Url);
                    }
                    catch (Exception e)
                    {
                        _logger.LogWarning(e, $"ClientService.Read, Client: {client.Title}, ClientCategory: {clientCategory.Id}, Url: {clientCategory.Url}");
                        continue;
                    }

                    var getDataList = new List<RssResult>();
                    foreach (var rssResult in result)
                    {
                        var story = await _storyRepository.Get(rssResult.Id, client.Id);
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
                            CategoryId = clientCategory.CategoryId,
                            Image = readerResult.Image,
                            Lead = readerResult.Lead,
                            Thumbnail = readerResult.Thumbnail,
                            Title = readerResult.Title,
                            PublishDateTime = readerResult.PublishDateTime,
                            IsActive = true
                        }).ToList();

                    await _storyRepository.AddRangeAsync(storyList);
                    await _storyRepository.SaveChangesAsync();
                }
            }
        }
    }
}
