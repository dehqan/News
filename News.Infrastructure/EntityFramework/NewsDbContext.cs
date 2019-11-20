using System.Reflection;
using Microsoft.EntityFrameworkCore;
using News.Domain.Model.Entity;

namespace News.Infrastructure.EntityFramework
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        { }

        public DbSet<Client> Client { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ClientCategory> ClientCategory { get; set; }
        public DbSet<Story> Story { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
