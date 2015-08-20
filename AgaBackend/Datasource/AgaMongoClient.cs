using System.Configuration;
using MongoDB.Driver;

namespace AgaBackend.Datasource
{
    class AgaMongoClient : IAgaMongoClient
    {
        private readonly MongoClient _client;

        public AgaMongoClient()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            _client = new MongoClient(connectionString);
        }

        public MongoClient Client
        {
            get { return _client; }
        }
    }
}