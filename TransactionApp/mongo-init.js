
db = db.getSiblingDB("data-transfer");
db.createCollection("transaction");

var count = 500_000;

var transactions = [];

for (var i = 0; i < count; i++) {
    transactions.push({ _id: crypto.randomUUID() });
}

db.transaction.insertMany(transactions);


db = db.getSiblingDB("data-transfer-new");
db.createCollection("transaction-sequential");
db.createCollection("transaction-pararallel");