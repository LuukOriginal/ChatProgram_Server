using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace ChatProgramServer.Models
{
    public class UserModel
    {
        Database db;
        public UserModel() 
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            db = new Database(config["DatabaseUri"]); //set's up the database
        }

        class User
        {
            public string username = "";
            public string[] conversations = {};
        }

        public string CreateUser(string rData) //creates a new user in the database
        {
            User netData = JsonConvert.DeserializeObject<User>(rData);

            BsonDocument data = netData.ToBsonDocument();
            db.Set(data);

            return data["_id"].ToString();
        }

        public async Task<string> GetUserFromName(string username) //Finds an user in the database
        {
            BsonDocument user = await db.Find("username", username);

            if (user == null) return "";

            var dotNetObj = BsonTypeMapper.MapToDotNetValue(user);
            string userData =  JsonConvert.SerializeObject(dotNetObj);

            return userData;
        }

    }
}
