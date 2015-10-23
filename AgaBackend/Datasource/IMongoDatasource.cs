using MongoDB.Driver;

namespace AgaBackend.Datasource
{
    public interface IMongoDatasource<T>
    {
        MongoCursor<T> Find(IMongoQuery query);
        void Save(T objectStore);
    }
}