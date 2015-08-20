using MongoDB.Driver;

namespace AgaBackend.Datasource
{
    public interface IAgaMongoServer
    {
        MongoServer Server { get; }
    }
}