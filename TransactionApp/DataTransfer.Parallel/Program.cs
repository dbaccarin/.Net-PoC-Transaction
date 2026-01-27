using DataTransfer.Parallel;
using MongoDB.Driver;
using System.Diagnostics;

string? connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MongoDb");

MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);

MongoClient client = new MongoClient(settings);

IMongoCollection<Transaction> transactionsCollection = client.GetDatabase("data-transfer").GetCollection<Transaction>("transaction");

var filter = Builders<Transaction>.Filter.Empty;

var transactions = await transactionsCollection.Find(filter).ToListAsync();

IMongoCollection<Transaction> transactionsNewCollection = client.GetDatabase("data-transfer-new").GetCollection<Transaction>("transaction-parallel");

var sw = Stopwatch.StartNew();

Console.WriteLine($"Starting data tranfer at {DateTime.Now}");
await Parallel.ForEachAsync(transactions, new ParallelOptions { MaxDegreeOfParallelism = 8 }, async (transaction, cancellationToken) =>
{
    await transactionsNewCollection.InsertOneAsync(transaction, cancellationToken: cancellationToken);
});

sw.Stop();

Console.WriteLine($"Finished in {sw.Elapsed.TotalSeconds} seconds. Transactions count:{transactions.Count}");
Console.WriteLine($"Ended data tranfer at {DateTime.Now}");