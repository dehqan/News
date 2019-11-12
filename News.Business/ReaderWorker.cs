using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using News.Core;
using News.Domain.Contracts.Repositories;

namespace News.Business
{
    public class ReaderWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReaderWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            
            var readerServices = scope.ServiceProvider.GetServices<IReaderService>().ToList();
            var clientRepository = scope.ServiceProvider.GetRequiredService<IClientRepository>();
            var clientList = await clientRepository.GetList();

            foreach (var client in clientList)
            {
                var readerService = readerServices.FirstOrDefault(x => x.GetType().Name.Contains(client.Title));
                if (readerService == null) continue;
                
                foreach (var clientCategory in client.ClientCategoryCollection)
                {
                    var result = await readerService.Read(clientCategory.Url);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return default;
        }
    }
}
