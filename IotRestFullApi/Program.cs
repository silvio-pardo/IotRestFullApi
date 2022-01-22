using IotRestFullApi.Dal;
using IotRestFullApi.Middlewares;
using IotRestFullApi.Models;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System;
using System.IO;

namespace IotRestFullApi
{
    public class Program
    {
        private static IConfiguration Configuration;
        private static PortConfigurations portConfigurations;
        public static void Main(string[] args)
        {
            LoadUpApp(args);
        }
        public static void LoadUpApp(string[] args)
        {
            Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("starting...");
                WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
                LoadConfiguration(builder.Configuration);
                ConfigureKestrel(logger,builder);
                ConfigureServices(builder);
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                WebApplication app = builder.Build();
                // Configure the HTTP request pipeline.
                ConfigureApp(app,builder.Environment);
                app.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            string connectionString = "";
            string envDocker = Environment.GetEnvironmentVariable("DATABASE_HOST");
            if (envDocker != null)
                connectionString = envDocker + Configuration.GetConnectionString("StaticParams");
            else
                connectionString = builder.Configuration.GetConnectionString("MigrationParam") + Configuration.GetConnectionString("StaticParams");

            builder.Services.AddDbContextPool<IotContext>(options =>
               options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            // load other services
            AddRepositories(builder.Services);
            ConfigureSwagger(builder.Services);
            // load grpc
            builder.Services.AddGrpc(_ =>
            {
                _.ResponseCompressionAlgorithm = "gzip";
            });
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ActionRepository>();
            services.AddScoped<CommandRepository>();
            services.AddScoped<DeviceRepository>();
            services.AddScoped<StatsRepository>();
        }
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IotApi v1", Version = "v1" });
            });
        }
        private static void ConfigureKestrel(Logger logger,WebApplicationBuilder builder)
        {
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenLocalhost(portConfigurations.RestPort, listenOptions =>
                    listenOptions.Protocols = HttpProtocols.Http1);

                options.ListenLocalhost( portConfigurations.GrpcPort, listenOptions => {
                    listenOptions.Protocols = HttpProtocols.Http2;
                    listenOptions.UseHttps("./../https/aspnetapp.pfx", "iotrestapi");
                });
            });
        }
        public static void ConfigureApp(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
        public static void LoadConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
            portConfigurations = configuration.GetSection(nameof(PortConfigurations)).Get<PortConfigurations>();
        }
    }
}
