using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MultiTenancy.Helpers;
using MultiTenancy.MiddleWare;
using MultiTenancy.Model;

namespace MultiTenancy
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

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITenantResolver, TenantResolver>();

            services.AddDbContext<MydbContext>((serviceProvider,dbContextBuilder)=>
            {
                var connectionStringPlaceHolder = Configuration.GetConnectionString("PlaceHolderConnection");
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var dbName = httpContextAccessor.HttpContext.Request.Headers["CURRENT_TENANT"].First();
                var connectionString = connectionStringPlaceHolder.Replace("{dbName}", dbName);
                dbContextBuilder.UseNpgsql(connectionString);
               



            }

                );

          // Configure Postgres Db
          

            services.AddControllers();
   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseTenantInjector();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
