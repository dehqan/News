using News.Core.EntityFramework.Entity;

namespace News.Domain.Model.Entity
{
    public class ClientCategory : EntityBase<int>
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}
