using System;

namespace News.Core.Models
{
    public class ReaderResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Lead { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string Link { get; set; }
    }
}
