using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TurismoMongoDB.Models
{
    public class Cidade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
