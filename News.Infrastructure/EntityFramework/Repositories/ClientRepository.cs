using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Domain.Contracts.Repositories;
using News.Domain.Model.Entity;

namespace News.Infrastructure.EntityFramework.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly NewsDbContext _dbContext;

        public ClientRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Client>> GetList()
        {
            return await _dbContext.Clients.AsNoTracking().Include(x => x.ClientCategoryCollection).ToListAsync();
        }
    }
}
