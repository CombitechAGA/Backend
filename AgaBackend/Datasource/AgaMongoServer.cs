using MongoDB.Driver;

namespace AgaBackend.Datasource
{
    public class AgaMongoServer : IAgaMongoServer
    {
        private readonly MongoServer _server;

        public AgaMongoServer(IAgaMongoClient client)
        {
            _server = client.Client.GetServer();
        }

        public MongoServer Server
        {
            get { return _server; }
        }
    }
}