using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            var apiAuthKey = configuration["Api-AuthKey"];
            var swaggerPath = context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase);

            if (swaggerPath || 
                (!string.IsNullOrEmpty(authHeader) && apiAuthKey.Equals(authHeader)))
            {
                await next.Invoke(context);
                return;
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}
