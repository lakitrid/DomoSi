using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DomoService.Dao
{
    public class MongoAbstractDao
    {
        protected readonly string mongoConnectionString;
        protected readonly string mongoDbName;

        protected MongoDatabase database;
        protected MongoClient client;
        protected MongoServer server;

        public MongoAbstractDao()
        {
            this.mongoConnectionString = ConfigurationManager.AppSettings["mongoConnectionString"];
            this.mongoDbName = ConfigurationManager.AppSettings["mongoDbName"];

            this.client = new MongoClient(this.mongoConnectionString);
            this.server = client.GetServer();
            this.database = server.GetDatabase(this.mongoDbName);
        }
    }
}
