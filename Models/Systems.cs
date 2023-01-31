using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MongoExample.Models
{
    public class Systems
    {
        [BsonElement("_id")]
        public string? ClientId { get; set; }

        [BsonElement("system_id")]
        public string SystemId { get; set; } = null!;
    }
}
