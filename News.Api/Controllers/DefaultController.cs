using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Clients.Farsnews;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IReader _reader;

        public DefaultController(IReader reader)
        {
            _reader = reader;
        }

        [HttpGet]
        public async Task Get()
        {
            await _reader.Read();
        }
    }
}