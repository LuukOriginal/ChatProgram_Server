using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProgramServer.Models
{
    public class UserModel
    {
        public UserModel() 
        { 

        }

        public class User
        {
            string username;
        }

        public string CreateUser(string rData) 
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            BsonDocument data = BsonSerializer.Deserialize<BsonDocument>(rData);

            Database db = new Database(config["DatabaseUri"]);
            db.Set(data);

            return data["_id"].ToString();
        }

    }
}
