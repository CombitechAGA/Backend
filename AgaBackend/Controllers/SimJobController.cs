using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using AgaBackend.Dto;
using AgaBackend.Services;

namespace AgaBackend.Controllers
{
    [RoutePrefix("api/get")]
    [EnableCors(origins: "http://localhost:55477", headers: "*", methods: "*")]
 
    public class SimJobController : ApiController
    {
        private readonly ISimJobService _simJobService;

        public SimJobController(ISimJobService simJobService) 
        {
            _simJobService = simJobService;
        }

        [Route("simjob")]
        public IHttpActionResult GetSimJobs() // show all available simulation jobs, started or not
        {
            var simJobList = _simJobService.GetSimJobs();
            return Ok(simJobList); 
            
        }
    }
}
