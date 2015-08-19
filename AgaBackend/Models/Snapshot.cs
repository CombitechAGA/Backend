using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
}