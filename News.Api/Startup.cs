using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using News.Business;
using News.Business.Clients.Services;
using News.Business.Services;
using News.Business.Services.Interfaces;
using News.Core;
using News.Core.Services;
using News.Core.Services.Interfaces;
using News.Domain.Contracts.Repositories;
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

            services.AddScoped<IReaderService, FarsnewsReaderService>();
            services.AddScoped<IReaderService, TasnimnewsReaderService>();
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IClientRepository, ClientRepository>();
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
