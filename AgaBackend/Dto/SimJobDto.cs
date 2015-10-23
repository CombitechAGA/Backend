using System;
using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;

namespace AgaBackend.Dto // SimJob Datatransfer Object
{
    public class SimJobDto
    {
        public ObjectId ObjectId { get; set; }  // simjob ID
        public string routeIDs { get; set; } // list of routes
        public Int16 speedFactor { get; set; } // speedfactor to multiply original speed with 
        public bool started { get; set; } // simjob started or not
        public bool repeat { get; set; } // repeat job or not

    }
}