
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Model
{
    public class MydbContext: DbContext
    {

        public MydbContext()
        {
        }

        public MydbContext(DbContextOptions options) : base(options)
        {

            Database.EnsureCreated();
        }
        
        public DbSet<Organisation> Organisations { get; set; }

       

    }
}
