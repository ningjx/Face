using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Face.Data.MongoDB
{
    class DBHelper
    {
        private static MongoClient client;
        private static IMongoDatabase database;
        private static IMongoCollection<BsonDocument> collection;
        //本地配置
        private const string MongoDBConnectionStr = "mongodb://localhost:27017";
        //数据库名称
        string dataBaseName = "Face";

        public DBHelper()
        {
            client = new MongoClient(MongoDBConnectionStr);
            database = client.GetDatabase(dataBaseName);
            collection = database.GetCollection<BsonDocument>("Data");
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="jObject"></param>
        public void SetData(BsonDocument bsonElements)
        {
            collection.InsertOne(bsonElements);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="fliter"></param>
        public IFindFluent<BsonDocument, BsonDocument> GetData(FilterDefinition<BsonDocument> filter)
        {
            //var filter = Builders<BsonDocument>.Filter.Eq("UserName", userName);
            IFindFluent<BsonDocument, BsonDocument> document = collection.Find(filter);
            return document;
        }
    }
}
