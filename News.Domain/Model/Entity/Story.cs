using System;
using News.Core.EntityFramework.Entity;

namespace News.Domain.Model.Entity
{
    public class Story : SupervisorEntityBase
    {
        public long ExternalId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Lead { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public DateTime PublishDateTime { get; set; }
    }
}
