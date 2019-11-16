using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.Models
{
    public class RssResult
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public DateTime? PubDate { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
    }
}
