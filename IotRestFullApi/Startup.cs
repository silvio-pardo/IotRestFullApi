using IotRestFullApi.Dal;
using IotRestFullApi.Middlewares;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace IotRestFullApi
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
            string connectionString = "";
            string envDocker = Environment.GetEnvironmentVariable("DATABASE_HOST");
            if (envDocker != null)
                connectionString = envDocker + Configuration.GetConnectionString("StaticParams");
            else
                connectionString = Configuration.GetConnectionString("MigrationParam") + Configuration.GetConnectionString("StaticParams");

            services.AddDbContextPool<IotContext>(options =>
               options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            AddRepositories(services);
        }

        public void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ActionRepository>();
            services.AddScoped<CommandRepository>();
            services.AddScoped<DeviceRepository>();
            services.AddScoped<StatsRepository>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
