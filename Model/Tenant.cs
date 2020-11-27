using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Model
{
    public class Tenant
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Hostname { get; set; }

        public string ConnectionString { get; set; }

        public string SecretKey { get; set; }
        public string APIKey { get; set; }
    }

    

}
