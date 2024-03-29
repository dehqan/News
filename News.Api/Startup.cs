using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using News.Business;
using News.Business.Services;
using News.Business.Services.Interfaces;
using News.Core;
using News.Core.Services;
using News.Core.Services.Interfaces;
using News.Domain.Contracts.Repositories;
using News.Infrastructure.Clients.Services;
using News.Infrastructure.EntityFramework;
using News.Infrastructure.EntityFramework.Repositories;

namespace News.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<NewsDbContext>(option => option.UseSqlServer(Configuration["Database:ConnectionString"]));

            #region Clients

            services.AddScoped<IReaderService, FarsnewsReaderService>();
            services.AddScoped<IReaderService, TasnimnewsReaderService>();
            services.AddScoped<IReaderService, MehrnewsReaderService>();
            services.AddScoped<IReaderService, IsnaReaderService>();
            services.AddScoped<IReaderService, YjcReaderService>();

            #endregion

            #region Repository

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            
            #endregion

            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IClientService, ClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
