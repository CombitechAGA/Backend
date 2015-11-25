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
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string zbeename { get; set; }
        public double distanceTraveled { get; set; }
    }
}