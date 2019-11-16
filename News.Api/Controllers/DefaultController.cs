using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Business.Services.Interfaces;
using News.Core;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IClientService _clientService;
        
        public DefaultController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task Get()
        {
            await _clientService.Read();
        }
    }
}