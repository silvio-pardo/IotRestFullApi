using IotRestFullApi.Dal;
using IotRestFullApi.Middlewares;
using IotRestFullApi.Models;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace IotRestFullApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly PortConfigurations portConfigurations;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            portConfigurations = configuration.GetSection(nameof(PortConfigurations)).Get<PortConfigurations>();
        }
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
            ConfigureSwagger(services);
            // load grpc
            services.AddGrpc(_ =>
            {
                _.ResponseCompressionAlgorithm = "gzip";
            });
        }
        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ActionRepository>();
            services.AddScoped<CommandRepository>();
            services.AddScoped<DeviceRepository>();
            services.AddScoped<StatsRepository>();
        }
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IotApi v1", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
;
            app.UseWhen(context => context.Connection.LocalPort == portConfigurations.GrpcPort,
                builder =>
                {
                    builder.UseRouting()
                    .UseEndpoints(endpoints =>
                    {
                        endpoints.MapGrpcService<StatsStreamingService>();
                    });
                }
            );
            app.UseWhen(context => context.Connection.LocalPort == portConfigurations.RestPort,
                builder =>
                {
                    builder.UseRouting()
                    .UseAuthorization()
                    .UseStaticFiles()
                    .UseMiddleware<AuthenticationMiddleware>()
                    .UseSwagger()
                    .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IotApi v1"))
                    .UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                }
            );
        }
    }
}
