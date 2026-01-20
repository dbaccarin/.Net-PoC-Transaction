using MongoDB.Driver;
using TransactionApp;


Console.WriteLine("Running container...");

MongoClientSettings settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27018");

MongoClient client = new MongoClient(settings);

IMongoCollection<Transaction> transactionsCollection = client.GetDatabase("local").GetCollection<Transaction>("transaction");

var filter = Builders<Transaction>.Filter.Empty;

var transactions = await transactionsCollection.Find(filter).ToListAsync();

foreach (var transaction in transactions)
{
    Console.WriteLine(transaction.Id);
}
