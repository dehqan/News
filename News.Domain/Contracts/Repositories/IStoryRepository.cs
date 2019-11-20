using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Domain.Model.Entity;

namespace News.Domain.Contracts.Repositories
{
    public interface IStoryRepository
    {
        Task<Story> Get(long externalId, int clientId);
        Task<int> SaveChangesAsync();
        Task AddRangeAsync(List<Story> storyList);
    }
}
