
db = db.getSiblingDB("app");
db.createCollection("transaction");

var count = 350000;

var transactions = [];

for (var i = 0; i < count; i++) {
    transactions.push({ _id: crypto.randomUUID() });
}

db.transaction.insertMany(transactions);


db = db.getSiblingDB("app-new");
db.createCollection("transaction-new");