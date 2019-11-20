using System;

namespace News.Core.EntityFramework.Entity
{
    public abstract class SupervisorEntityBase : SupervisorEntityBase<long>, IEntityBase
    {
    }

    public abstract class SupervisorEntityBase<T> : EntityBase<T> , ISupervisorEntityBase
    {
        public DateTime CreateDateTime { get; set; }
    }
}
