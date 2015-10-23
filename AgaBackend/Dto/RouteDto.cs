using System;
using MongoDB.Bson;
using AgaBackend.Models;
//using MongoDB.Bson.Serialization.Attributes;

namespace AgaBackend.Dto // SimJob Datatransfer Object
{
    public class RouteDto
    {
        public ObjectId RouteId { get; set; }  // route ID
        public string CarId { get; set; }
        public DateTime Date { get; set; }
       // public List<Snapshot> dat;
    }
}