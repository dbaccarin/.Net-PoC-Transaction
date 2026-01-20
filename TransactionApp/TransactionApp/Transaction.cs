using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionApp
{   
    [BsonIgnoreExtraElements]
    public class Transaction
    {
        [BsonId]
        [BsonElement("_id")]
        public Guid Id { get; set; }
    }
}
