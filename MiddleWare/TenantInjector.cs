using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MultiTenancy.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TenantInjector
    {
        private readonly RequestDelegate _next;

        public TenantInjector(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var tenant = string.Empty;

            if (httpContext.Request.Host.Host.Contains("."))
            {
                tenant = httpContext.Request.Host.Host.Split('.')[0].ToLowerInvariant();
                httpContext.Items.Add("CURRENT_TENANT", tenant);
            }
            else
            {
               httpContext.Items.Add("CURRENT_TENANT", httpContext.Request.Host.Host);
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TenantInjectorExtensions
    {
        public static IApplicationBuilder UseTenantInjector(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantInjector>();
        }
    }
}
