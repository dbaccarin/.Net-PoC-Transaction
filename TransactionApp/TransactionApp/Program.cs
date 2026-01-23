using MongoDB.Driver;
using System.Diagnostics;
using TransactionApp;

string? connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MongoDb");

MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);

MongoClient client = new MongoClient(settings);

IMongoCollection<Transaction> transactionsCollection = client.GetDatabase("app").GetCollection<Transaction>("transaction");

var filter = Builders<Transaction>.Filter.Empty;

var transactions = await transactionsCollection.Find(filter).ToListAsync();

IMongoCollection<Transaction> transactionsNewCollection = client.GetDatabase("app-new").GetCollection<Transaction>("transaction-new");

//foreach (var transaction in transactions)
//{
//    transactionsNewCollection.InsertOne(transaction);
//    Console.WriteLine(transaction.Id);
//}

var sw = Stopwatch.StartNew();

await Parallel.ForEachAsync(transactions, new ParallelOptions { MaxDegreeOfParallelism = 8 }, async (transaction, cancellationToken) =>
{
    // Each thread sends a batch of 10,000 documents
    await transactionsNewCollection.InsertOneAsync(transaction, cancellationToken: cancellationToken);
});

sw.Stop();

Console.WriteLine($"Finished in {sw.Elapsed.TotalSeconds} .Transactions count:{transactions.Count}");
