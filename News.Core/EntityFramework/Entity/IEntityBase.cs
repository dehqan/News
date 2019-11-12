using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.EntityFramework.Entity
{
    public interface IEntityBase : IEntityBase<long>
    {
    }

    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }
}
