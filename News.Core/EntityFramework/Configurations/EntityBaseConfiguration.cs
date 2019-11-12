using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Core.EntityFramework.Entity;

namespace News.Core.EntityFramework.Configurations
{
    public abstract class EntityBaseConfiguration<TEntity> : EntityBaseConfiguration<TEntity, long> where TEntity : class, IEntityBase
    {
    }

    public abstract class EntityBaseConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntityBase<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);
        }
    }
}
