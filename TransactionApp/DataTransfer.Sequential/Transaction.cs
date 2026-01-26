using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataTransfer.Sequential
{
    [BsonIgnoreExtraElements]
    public class Transaction
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        [BsonElement("_id")]
        public Guid Id { get; set; }
    }
}