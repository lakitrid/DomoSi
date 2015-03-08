using DomoHard.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomoHardServ
{
    /// <summary>
    /// Add sensor data in MongoDb set
    /// </summary>
    /// <typeparam name="T">type of data</typeparam>
    internal class SaveData<T> : IDisposable where T : SensorData
    {
        private readonly string connectionString;
        private readonly string dbName;

        private MongoDatabase database;
        private MongoClient client;
        private MongoServer server;

        /// <summary>
        /// Prepare the connection
        /// </summary>
        /// <param name="connectionString">connection string</param>
        /// <param name="dbName">database name</param>
        public SaveData(string connectionString, string dbName)
        {
            this.connectionString = connectionString;
            this.dbName = dbName;
            this.client = new MongoClient(this.connectionString);
            this.server = client.GetServer();
            this.database = server.GetDatabase(this.dbName);
        }

        /// <summary>
        /// Add data sample in mongoDb data set
        /// </summary>
        /// <param name="data">data to add</param>
        public void AddData(T data)
        {
            var collection = database.GetCollection<T>(data.Type);
            collection.Insert(data);
        }

        /// <summary>
        /// Dispose of the db connection
        /// </summary>
        public void Dispose()
        {
            this.server.Disconnect();
        }
    }
}
