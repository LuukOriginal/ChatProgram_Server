using MongoDB.Driver;
using MongoDB.Bson;

namespace ChatProgramServer
{
    public class Database
    {
        IMongoCollection<BsonDocument> Collection;

        public Database(string ConnectionUri) //Initialize the database handler
        {
            var settings = MongoClientSettings.FromConnectionString(ConnectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);

            IMongoDatabase UserDatabase = client.GetDatabase("ChatAppMain");
            Collection = UserDatabase.GetCollection<BsonDocument>("Users");
        }

        public async Task<BsonDocument> Find(string key, string value) //finding values inside of the database
        {
            var filter = Builders<BsonDocument>.Filter.Eq(key, value);

            var user = await Collection.Find(filter).FirstOrDefaultAsync();

            if (user != null)
            {
                return user;
            }
            else
            {
                return new BsonDocument();
            }
        }

        public void Set(BsonDocument value) //set values inside of the database
        {
            try
            {
                Collection.InsertOne(value);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
