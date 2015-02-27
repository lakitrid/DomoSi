using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DomoDataServ
{
    public sealed class DataLogger
    {
        private static readonly DataLogger instanceValue = new DataLogger();

        private readonly string connectionstring = "mongodb://WHS";

        private MongoDatabase database;

        private DataLogger()
        {
            var client = new MongoClient(this.connectionstring);
            var server = client.GetServer();
            this.database = server.GetDatabase("test");
        }

        public static DataLogger Instance { get { return instanceValue; } }

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
    }
}
