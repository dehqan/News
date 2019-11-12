using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News.Core;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly Dictionary<string, string> Clients = new Dictionary<string, string>
        {
            {"https://www.farsnews.com/rss/economy", "Farsnews" },
            {"https://www.tasnimnews.com/fa/rss/feed/0/7/1/%D8%B3%DB%8C%D8%A7%D8%B3%DB%8C", "Tasnimnews" },
            {"https://www.farsnews.com/rss/world", "Farsnews" },
            {"https://www.tasnimnews.com/fa/rss/feed/0/7/3/%D9%88%D8%B1%D8%B2%D8%B4%DB%8C", "Tasnimnews" }
        };
        private readonly IEnumerable<IReader> _readerServices;

        public DefaultController(IEnumerable<IReader> readerServices)
        {
            _readerServices = readerServices;
        }

        [HttpGet]
        public async Task Get()
        {
            foreach (var client in Clients)
            {
                var readerService = _readerServices.FirstOrDefault(x => x.GetType().Namespace.Contains(client.Value));
                if (readerService != null)
                {
                    var result = await readerService.Read(client.Key);
                }
            }
        }
    }
}