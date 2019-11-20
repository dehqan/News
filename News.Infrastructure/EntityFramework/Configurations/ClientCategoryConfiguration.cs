using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Core.EntityFramework.Configurations;
using News.Domain.Model.Entity;

namespace News.Infrastructure.EntityFramework.Configurations
{
    public class ClientCategoryConfiguration : EntityBaseConfiguration<ClientCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ClientCategory> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Url).HasMaxLength(1000);

        }
    }
}
