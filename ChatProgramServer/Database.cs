using MongoDB.Driver;
using MongoDB.Bson;

namespace ChatProgramServer
{
    public class Database
    {
        IMongoCollection<BsonDocument> Collection;

        public Database(string ConnectionUri) 
        {
            var settings = MongoClientSettings.FromConnectionString(ConnectionUri);
            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);

            IMongoDatabase UserDatabase = client.GetDatabase("ChatAppMain");
            Collection = UserDatabase.GetCollection<BsonDocument>("Users");
        }

        public async void Get(string key)
        {
            Console.WriteLine("test");
            var documents = await Collection.Find(new BsonDocument()).ToListAsync();
            foreach (var document in documents)
            {
                Console.WriteLine(document.ToString());
            }
        }

        public void Set(string key, BsonDocument value) 
        {
            try
            {
                Collection.InsertOne(value);

                Console.WriteLine(value["_id"].ToString());
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
