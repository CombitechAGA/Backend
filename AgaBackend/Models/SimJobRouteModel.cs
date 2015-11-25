using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AgaBackend.Models
{
    [BsonIgnoreExtraElements]

    public class SimJobRouteModel
    {
        public SimJobRouteModel()
        {
            Snapshots = new List<Snapshot>();
        }
        [BsonId]
        public string RouteId { get; set; }
        public string carID { get; set; }
        public DateTime timestamp { get; set; } // timestamp of last (newest) snapshot 

        public List<Snapshot> Snapshots { get; set; }

        // list of snapshot follows........
    }
}