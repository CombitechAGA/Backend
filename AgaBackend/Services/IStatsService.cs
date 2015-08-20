using System;
using System.Collections.Generic;
using AgaBackend.Dto;

namespace AgaBackend.Controllers
{
    public interface IStatsService
    {
        List<AverageSpeedDto> GetAverageSpeeds(DateTime from, DateTime to);
    }
}