
using MultiTenancy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Helpers
{
    interface ITenantResolver
    {
        string CurrentTenant();
    }

    public class TenantResolver : ITenantResolver
    {

        private string Current_Tenant { get; set; }

        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
  

        public TenantResolver(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
         
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentTenant()
        {

            if (string.IsNullOrEmpty(Current_Tenant))
            {
                SetTenant();
            }
            return Current_Tenant;
        }

        public void SetTenant()
        {

            _httpContextAccessor.HttpContext.Items.TryGetValue("CURRENT_TENANT", out object currentTenant);
            if (currentTenant != null)
            {
                Current_Tenant = currentTenant.ToString();
            }
        }
    }




}
