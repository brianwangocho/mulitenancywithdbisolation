﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Model
{

    public class Tenant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hostname { get; set; }

        public string ConnectionString { get; set; }

        public string SecretKey { get; set; }
        public string APIKey { get; set; }
    }

    

}
