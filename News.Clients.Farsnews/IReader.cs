using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace News.Clients.Farsnews
{
    public interface IReader
    {
        Task Read();
    }
}
