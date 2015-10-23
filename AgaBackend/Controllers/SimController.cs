using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using AgaBackend.Dto;
using AgaBackend.Services;

namespace AgaBackend.Controllers
{
    [RoutePrefix("api/simulator")]
    [EnableCors(origins: "http://localhost:55477", headers: "*", methods: "*")]
    public class SimController : ApiController
    {
        private readonly ISimCarService _simService;

        public SimController(ISimCarService simService) 
        {
            _simService = simService;
        }

        [Route("routes")]
        public IHttpActionResult GetRoutes(DateTime from) // show all available routes: 1. get all saved routes 2. identify new routes 3. save new routes 4.  show all routes
        {

            var routeList = _simService.GetRoutes(from); // list all stored and new routes from speciified date
            return Ok(routeList); 
            
        }
    }
}

