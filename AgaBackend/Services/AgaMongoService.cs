using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgaBackend.Dto;
using AgaBackend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace AgaBackend.Services
{
    public class AgaMongoService
    {
        private readonly string _database;

        public AgaMongoService(string connectionstring, string database)
        {
            _database = database;
            _client = new MongoClient(connectionstring);
        }

        private MongoClient _client;

        public List<AverageSpeedDto> GetAverageSpeeds(DateTime from, DateTime to)
        {
            var server = _client.GetServer();
            var db = server.GetDatabase(_database);
            var col = db.GetCollection<Snapshot>("snapshot");


            var query = Query.And(
                Query.GTE("timestamp", from),
                Query.LTE("timestamp", to));

            var result = col.Find(query);//.OrderBy(f=>f.Timestamp);
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