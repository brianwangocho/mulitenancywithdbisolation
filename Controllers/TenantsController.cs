using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenancy.Model;

namespace MultiTenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        public HostDbContext _Db;

        public TenantsController(HostDbContext Db)
        {
            _Db = Db;
        }

        /// <summary>
        /// /URL: api/Organisation/organisation_list
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("tenant_list")]
        [Route("tenant_list")]
        public async Task<IActionResult> GetOrganisations()
        {
            var result = _Db.Tenants;

            return Ok(result);


        }
        [HttpPost("add_tenant")]
        [Route("add_tenant")]
        public async Task<IActionResult> AddTenant(Request request)
        {
            Tenant tenant = new Tenant();
            tenant.Name = request.Name;
            tenant.Hostname = request.Hostname;
            tenant.SecretKey = request.SecretKey;
            tenant.APIKey = request.APIKey;
            tenant.ConnectionString = request.ConnectionString;
            _Db.Tenants.Add(tenant);
           await _Db.SaveChangesAsync();
            
            return Ok();

        }
        [HttpPost("get_tenant")]
        [Route("get_tenant")]
        public async Task<IActionResult> GetTenant()
        {
       
            
       
            return Ok(_Db.Tenants);

        }
    }
}

