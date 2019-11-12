using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using News.Domain.Model.Entity;

namespace News.Domain.Contracts.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetList();
    }
}
