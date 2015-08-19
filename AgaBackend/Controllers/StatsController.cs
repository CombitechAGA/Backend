using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using AgaBackend.Dto;
using AgaBackend.Services;

namespace AgaBackend.Controllers
{
    [RoutePrefix("api/stats")]
    public class StatsController : ApiController
    {
        [Route("speed/average")]
        public IHttpActionResult Get(DateTime from, DateTime to)
        {
            var averageSpeeds = GetAverageSpeeds(from, to);
            return Ok(averageSpeeds);
        }

        private List<AverageSpeedDto> GetAverageSpeeds(DateTime from, DateTime to)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;

            AgaMongoService ams = new AgaMongoService(connectionString, "telemetry");
            return ams.GetAverageSpeeds(from, to);
        }
    }
}
