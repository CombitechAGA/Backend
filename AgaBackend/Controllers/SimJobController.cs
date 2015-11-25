using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using AgaBackend.Dto;
using AgaBackend.Models;
using AgaBackend.Services;

namespace AgaBackend.Controllers
{
    [RoutePrefix("api/simjobs")]
    [EnableCors(origins: "http://localhost:55477", headers: "*", methods: "*")]
 
    public class SimJobController : ApiController
    {
        private readonly ISimJobService _simJobService;

        public SimJobController(ISimJobService simJobService) 
        {
            _simJobService = simJobService;
        }

        public IHttpActionResult Get() // show all available simulation jobs, started or not <res:query/get array>
        {
            var simJobList = _simJobService.GetSimJobs();
            return Ok(simJobList); 
        }
        /*
        public IHttpActionResult Get(SimJobModel simJob) // show all available simulation jobs, started or not <res:query/get array>
        {
            var simJobObject = _simJobService.GetSimJob(simJob);
            return Ok(simJobObject);
        }*/
        public IHttpActionResult Post(SimJobModel simObject) // <res:save>
        {
            _simJobService.SaveSimJob(simObject); // should bve SimJobModel as param to function
            return Ok();
        }

        public IHttpActionResult Delete(SimJobModel simObject) // <res:remove>  TODO: BUG: simObject is always NULL!
        {
            if (simObject != null)
            { 
                _simJobService.RemoveSimJob(simObject); // should bve SimJobModel as param to function
            }
            return Ok();
        }

        [Route("{jobId}")]
        public IHttpActionResult Get(string jobId) // get started simjobs
        {
            var simJobList = _simJobService.GetStartedSimJobs();
            return Ok(simJobList);

            //return Ok(new SimJobModel());
        }

  

    }
}
