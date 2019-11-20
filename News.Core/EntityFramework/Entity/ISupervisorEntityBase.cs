using System;
using System.Collections.Generic;
using System.Text;

namespace News.Core.EntityFramework.Entity
{
    public interface ISupervisorEntityBase
    {
        DateTime CreateDateTime { get; set; }
    }
}
