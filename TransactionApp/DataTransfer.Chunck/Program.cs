using DataTransfer.Chunck;
using MongoDB.Driver;
using System.Diagnostics;

string? connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MongoDb");

MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);

MongoClient client = new MongoClient(settings);

IMongoCollection<Transaction> transactionsCollection = client.GetDatabase("data-transfer").GetCollection<Transaction>("transaction");

var filter = Builders<Transaction>.Filter.Empty;

var transactions = await transactionsCollection.Find(filter).ToListAsync();

IMongoCollection<Transaction> transactionsNewCollection = client.GetDatabase("data-transfer-new").GetCollection<Transaction>("transaction-chunck");

var sw = Stopwatch.StartNew();

Console.WriteLine($"Starting data transfer at {DateTime.Now}");

foreach (var chunk in transactions.Chunk(10_000))
{
    await transactionsNewCollection.InsertManyAsync(chunk);
}

sw.Stop();

Console.WriteLine($"Finished in {sw.Elapsed.TotalSeconds} seconds. Transactions count:{transactions.Count}");
Console.WriteLine($"Ended data transfer at {DateTime.Now}");