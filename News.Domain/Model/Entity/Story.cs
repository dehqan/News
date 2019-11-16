using System;
using News.Core.EntityFramework.Entity;

namespace News.Domain.Model.Entity
{
    public class Story : SupervisorEntityBase
    {
        public string ExternalId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Lead { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }
        public DateTime? PublishDateTime { get; set; }
        public int? PublishDateTimeMiliseconds { get; set; }
        public string PublishDateTimePersian { get; set; }
        public string PublishDateTimePersianFull { get; set; }
    }
}
