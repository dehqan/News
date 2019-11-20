using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Core.EntityFramework.Configurations;
using News.Domain.Model.Entity;

namespace News.Infrastructure.EntityFramework.Configurations
{
    public class StoryConfiguration : SupervisorEntityBaseConfiguration<Story>
    {
        public override void Configure(EntityTypeBuilder<Story> builder) 
        {
            base.Configure(builder);

            builder.Property(x => x.Title).HasMaxLength(200);
            builder.Property(x => x.Lead).HasMaxLength(500);
            builder.Property(x => x.Thumbnail).HasMaxLength(1000);
            builder.Property(x => x.Image).HasMaxLength(1000);
            builder.Property(x => x.Link).HasMaxLength(1000);
        }
    }
}
