using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AgaBackend.Models
{
    [BsonIgnoreExtraElements]
    public class Snapshot
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public string carID { get; set; }
        public double speed { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class RouteModel
    {
        public RouteModel()
        {
            Snapshots = new List<Snapshot>();
        }
        [BsonId]
        public ObjectId RouteId { get; set; }
        public string carID { get; set; }
        public DateTime timestamp { get; set; } // timestamp of last (newest) snapshot 

        public List<Snapshot> Snapshots { get; set; }

        // list of snapshot follows........
    }

    public class SimJobModel
    {
        public SimJobModel()
        {
            Routes = new List<RouteModel>();
        }
        [BsonId]
        public ObjectId SimJobId { get; set; }
        public bool repeatJob { get; set; } 
        public UInt16 speedX { get; set; } // speed simulation multiplier  
        public bool jobStarted { get; set; }

        public List<RouteModel> Routes { get; set; }
    }
}