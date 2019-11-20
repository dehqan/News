using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.Domain.Contracts.Repositories;
using News.Domain.Model.Entity;

namespace News.Infrastructure.EntityFramework.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly NewsDbContext _context;

        public ClientRepository(NewsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetList()
        {
            return await _context.Client.AsNoTracking().Include(x => x.ClientCategoryCollection).Where(x => x.IsActive).ToListAsync();
        }
    }
}
