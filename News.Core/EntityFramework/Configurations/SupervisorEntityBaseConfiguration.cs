using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Core.EntityFramework.Entity;

namespace News.Core.EntityFramework.Configurations
{
    public abstract class SupervisorEntityBaseConfiguration<TEntity> : EntityBaseConfiguration<TEntity> where TEntity : class, IEntityBase, ISupervisorEntityBase 
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CreateDateTime)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
