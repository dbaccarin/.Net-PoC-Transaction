
db = db.getSiblingDB("local");
db.createCollection("transaction");

//db.transaction.insertOne({ _id: "3fa85f64-5717-4562-b3fc-2c963f66afa6" })

db.transaction.insertMany(
    [
        { _id: "3fa85f64-5717-4562-b3fc-2c963f66afa6" },
        { _id: "3fa85f64-5717-4562-b3fc-2c963f66afa7" },
    ]
);
