using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Core.EntityFramework.Configurations;
using News.Domain.Model.Entity;

namespace News.Infrastructure.EntityFramework.Configurations
{
    public class ClientConfiguration : EntityBaseConfiguration<Client, int>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200);

        }
    }
}
