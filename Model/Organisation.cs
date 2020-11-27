using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Model
{
    public class Organisation
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateTimeModified { get; set; }

        public string Status { get; set; }

    }
}
