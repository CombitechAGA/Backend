using MongoDB.Driver;

namespace AgaBackend.Datasource
{
    public class MongoDataSource<T> : IMongoDatasource<T> where T : new()
    {
        private readonly MongoCollection<T> _collection;

        public MongoDataSource(IAgaMongoServer mongoServer, DatasourceParams datasourceParams)
        {
            var database = mongoServer.Server.GetDatabase(datasourceParams.DatabaseName);
            if (!database.CollectionExists(datasourceParams.CollectionName))
            {
                database.CreateCollection(datasourceParams.CollectionName);
            }
            _collection = database.GetCollection<T>(datasourceParams.CollectionName);
        }

        public MongoCursor<T> Find(IMongoQuery query)
        {
            return _collection.Find(query);
        }

        public void Save(T objectStore)
        {
            _collection.Save(objectStore);
        }

        public void RemoveAll()
        {
            _collection.RemoveAll();
        }

        public void Remove(IMongoQuery query)
        {
            _collection.Remove(query);
        }

    }
}