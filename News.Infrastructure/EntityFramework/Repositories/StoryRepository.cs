using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Domain.Contracts.Repositories;
using News.Domain.Model.Entity;

namespace News.Infrastructure.EntityFramework.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly NewsDbContext _context;

        public StoryRepository(NewsDbContext context)
        {
            _context = context;
        }

        public async Task<Story> Get(long externalId, int clientId)
        {
            return await _context.Story.FirstOrDefaultAsync(x => x.ExternalId == externalId && x.ClientId == clientId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<Story> storyList)
        {
            await _context.Story.AddRangeAsync(storyList);
        }
    }
}
