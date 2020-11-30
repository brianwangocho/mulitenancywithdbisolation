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
    public class OrganisationController : ControllerBase
    {

        public HostDbContext _Db;

        public OrganisationController(HostDbContext Db)
        {
            _Db = Db;
        }

        /// <summary>
        /// /URL: api/Organisation/organisation_list
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("organisation_list")]
        [Route("organisation_list")]
        public  Task<IActionResult> GetOrganisations()
        {
            var result =  _Db.Organisations.ToList<Organisation>();

            return Ok(result);


        }
    }
}
