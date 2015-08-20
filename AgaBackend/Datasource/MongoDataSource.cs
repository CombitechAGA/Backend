﻿using MongoDB.Driver;

namespace AgaBackend.Datasource
{
    public class MongoDataSource<T> : IMongoDatasource<T> where T : new()
    {
        private readonly MongoCollection<T> _collection; 

        public MongoDataSource(IAgaMongoServer mongoServer, DatasourceParams datasourceParams)
        {
            var database = mongoServer.Server.GetDatabase(datasourceParams.DatabaseName);
            _collection = database.GetCollection<T>(datasourceParams.CollectionName);
        }

        public MongoCursor<T> Find(IMongoQuery query)
        {
            return _collection.Find(query);
        }
    }
}