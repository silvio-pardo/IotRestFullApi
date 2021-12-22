using IotCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IotRestFullApi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> logger;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(httpContext);
            }
            else
            {
                string authHeader = httpContext.Request.Headers["Authorization"];
                if (authHeader != null)
                {
                    if (authHeader == EnvParams.ApiKey)
                    {
                        logger.LogInformation(httpContext.Request.Path.Value);
                        await _next(httpContext);
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 401;
                        return;
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }
            }
        }
    }
}
