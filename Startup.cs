using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MultiTenancy.Helpers;

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

            services.AddDbContext<HostDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("HostConnection"));

            });


            services.AddDbContext<MydbContext>((serviceProvider, dbContextBuilder) =>
            {
                var connectionStringPlaceHolder = Configuration.GetConnectionString("PlaceHolderConnection");
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            
                HostDbContext hostDbContext = new HostDbContext();
                //dbName = httpContextAccessor.HttpContext.Request.Headers["CURRENT_TENANT"].First();

                var connectionString = "";
                var OrganisationId = httpContextAccessor.HttpContext.Request.Headers["CURRENT_TENANT"].First();

               Organisation organisation = hostDbContext.Organisations.Where(a => a.Id ==Int32.Parse(OrganisationId)).FirstOrDefault();

              //  dbName = organisation.Name;
                connectionString = connectionStringPlaceHolder.Replace("{dbName}",organisation.Name);
                dbContextBuilder.UseNpgsql(connectionString);
            }

                );

            // Identity FOR CORE DB
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            


            }).AddEntityFrameworkStores<HostDbContext>();

            /// TENANT DB
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;


            }).AddEntityFrameworkStores<MydbContext>();


            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    RequireExpirationTime = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true


                };
            });


            services.AddControllers();
   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,HostDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //   app.UseTenantInjector();
            db.Database.EnsureCreated();
           

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
