using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News.Business.Services.Interfaces;
using News.Core;
using News.Domain.Contracts.Repositories;

namespace News.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IEnumerable<IReaderService> _readerServices;
        private readonly IClientRepository _clientRepository;

        public ClientService(IEnumerable<IReaderService> readerServices, IClientRepository clientRepository)
        {
            _readerServices = readerServices;
            _clientRepository = clientRepository;
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
                    var result = await readerService.Read(clientCategory.Url);
                }
            }
        }
    }
}
