using System.Collections.Generic;
using News.Core.EntityFramework.Entity;

namespace News.Domain.Model.Entity
{
    public class Category : EntityBase<int>
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ClientCategory> ClientCategoryCollection { get; set; }
    }
}
