using MongoDB.Driver;
using TransactionApp;


Console.WriteLine("Running app...");

MongoClientSettings settings = MongoClientSettings.FromConnectionString("mongodb://mongodb-database:27017");

MongoClient client = new MongoClient(settings);

IMongoCollection<Transaction> transactionsCollection = client.GetDatabase("local").GetCollection<Transaction>("transaction");

var filter = Builders<Transaction>.Filter.Empty;

var transactions = await transactionsCollection.Find(filter).ToListAsync();

foreach (var transaction in transactions)
{
    Console.WriteLine(transaction.Id);
}
