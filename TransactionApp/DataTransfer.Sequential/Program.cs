using DataTransfer.Sequential;
using MongoDB.Driver;
using System.Diagnostics;

string? connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MongoDb");

MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);

MongoClient client = new MongoClient(settings);

IMongoCollection<Transaction> transactionsCollection = client.GetDatabase("data-transfer").GetCollection<Transaction>("transaction");

var filter = Builders<Transaction>.Filter.Empty;

var transactions = await transactionsCollection.Find(filter).ToListAsync();

IMongoCollection<Transaction> transactionsNewCollection = client.GetDatabase("data-transfer-new").GetCollection<Transaction>("transaction-sequential");

Console.WriteLine($"Starting data tranfer at {DateTime.Now}");
var sw = Stopwatch.StartNew();

foreach (var transaction in transactions)
{
    transactionsNewCollection.InsertOne(transaction);
}

sw.Stop();

Console.WriteLine($"Finished in {sw.Elapsed.TotalSeconds} seconds.Transactions count:{transactions.Count}");
Console.WriteLine($"Ended data tranfer at {DateTime.Now}");
