using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Model
{
    public class RegisterRequest
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
