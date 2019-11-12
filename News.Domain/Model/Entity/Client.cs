using System.Collections.Generic;
using News.Core.EntityFramework.Entity;

namespace News.Domain.Model.Entity
{
    public class Client : EntityBase<int>
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ClientCategory> ClientCategoryCollection { get; set; }
    }
}
