﻿using System;
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
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [Route("speed/average")]
        public IHttpActionResult Get(DateTime from, DateTime to)
        {
            var averageSpeeds = _statsService.GetAverageSpeeds(from, to);
            return Ok(averageSpeeds);
        }
    }
}
