using DataTransfer.Sequential;
using MongoDB.Driver;
using System.Diagnostics;

string? connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MongoDb");

MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);

MongoClient client = new MongoClient(settings);

IMongoCollection<Transaction> transactionsCollection = client.GetDatabase("app").GetCollection<Transaction>("transaction");

var filter = Builders<Transaction>.Filter.Empty;

var transactions = await transactionsCollection.Find(filter).ToListAsync();

IMongoCollection<Transaction> transactionsNewCollection = client.GetDatabase("app-new").GetCollection<Transaction>("transaction-new");

var sw = Stopwatch.StartNew();


foreach (var transaction in transactions)
{
    transactionsNewCollection.InsertOne(transaction);
    Console.WriteLine(transaction.Id);
}

sw.Stop();


//await Parallel.ForEachAsync(transactions, new ParallelOptions { MaxDegreeOfParallelism = 8 }, async (transaction, cancellationToken) =>
//{
//    // Each thread sends a batch of 10,000 documents
//    await transactionsNewCollection.InsertOneAsync(transaction, cancellationToken: cancellationToken);
//});


Console.WriteLine($"Finished in {sw.Elapsed.TotalSeconds} .Transactions count:{transactions.Count}");
