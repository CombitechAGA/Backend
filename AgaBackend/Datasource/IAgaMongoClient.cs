using MongoDB.Driver;

namespace AgaBackend.Datasource
{
    public interface IAgaMongoClient
    {
        MongoClient Client { get; }
    }
}