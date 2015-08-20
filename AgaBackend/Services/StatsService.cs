using System;
using System.Collections.Generic;
using System.Linq;
using AgaBackend.Controllers;
using AgaBackend.Datasource;
using AgaBackend.Dto;
using AgaBackend.Models;
using MongoDB.Driver.Builders;

namespace AgaBackend.Services
{
    public class StatsService: IStatsService
    {
        private readonly IMongoDatasource<Snapshot> _datasource;

        public StatsService(IMongoDatasource<Snapshot> datasource)
        {
            _datasource = datasource;
        }

        public List<AverageSpeedDto> GetAverageSpeeds(DateTime from, DateTime to)
        {
            var query = Query.And(
                Query.GTE("timestamp", from),
                Query.LTE("timestamp", to));

            var result = _datasource.Find(query);//.OrderBy(f=>f.Timestamp);
            var dates = result.GroupBy(f => f.timestamp.Date);

            var listAverageSpeeds = new List<AverageSpeedDto>();

            foreach (var day in dates)
            {
                var cars = day.GroupBy(f => f.carID);
                foreach (var car in cars)
                {
                    var averageSpeedItem = new AverageSpeedDto
                    {
                        Date = day.Key,
                        AverageSpeed = day.Where(c=>c.carID==car.Key).Average(f => f.speed),
                        CarId = car.Key
                    };

                    listAverageSpeeds.Add(averageSpeedItem);
                }
            }

            return listAverageSpeeds;
        }
    }
}