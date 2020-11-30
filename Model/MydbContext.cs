
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Model
{
    public class MydbContext: IdentityDbContext
    {

        public MydbContext()
        {
            Database.EnsureCreated();
        }

        public MydbContext(DbContextOptions<MydbContext> options) : base(options)
        {
            Database.EnsureCreated();

        }
        public DbSet<Branches> Branches { get; set; }




    }


    public class HostDbContext : IdentityDbContext
    {

        public HostDbContext()
        {
                    

        }

        public HostDbContext(DbContextOptions<HostDbContext> options) : base(options)
        {

        


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("HostConnection");
                optionsBuilder.UseNpgsql(connectionString);
               ;
            }
        }
        public DbSet<Organisation> Organisations { get; set; }

           public DbSet<Tenant> Tenants { get; set; }




    }
}
