using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DomoDataServ
{
    public sealed class DataLogger : IDisposable
    {
        private readonly string connectionString;
        private readonly string dbName;

        private MongoDatabase database;
        private MongoClient client;
        private MongoServer server;

        public DataLogger(string connectionString, string dbName)
        {
            this.connectionString = connectionString;
            this.dbName = dbName;
            this.client = new MongoClient(this.connectionString);
            this.server = client.GetServer();
            this.database = server.GetDatabase(dbName);
        }

        public DataLog AddData(DataLog data)
        {
            data.Id = Guid.NewGuid();

            var collection = database.GetCollection<DataLog>("datalog");
            collection.Insert(data);

            return data;
        }

        public List<DataLog> GetData(string type, DateTime? start, DateTime? end)
        {
            List<DataLog> logs = new List<DataLog>();

            var collection = database.GetCollection<DataLog>("datalog");

            var query = Query<DataLog>.EQ(e => e.Type, type);
            logs.AddRange(collection.Find(query).ToList());

            return logs;
        }

        public void RemoveAll(string type)
        {
            var collection = database.GetCollection<DataLog>("datalog");
            
            var query = Query<DataLog>.EQ(e => e.Type, type);
            collection.Remove(query);
        }

        public void Dispose()
        {
            this.server.Disconnect();
        }
    }
}
