using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Core;
using News.Domain.Contracts.Repositories;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IEnumerable<IReaderService> _readerServices;
        private readonly IClientRepository _clientRepository;

        public ReaderController(IEnumerable<IReaderService> readerServices, IClientRepository clientRepository)
        {
            _readerServices = readerServices;
            _clientRepository = clientRepository;
        }

        public async Task Read()
        {
            
        }
    }
}