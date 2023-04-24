using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TurismoMongoDB.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public Endereco endereco { get; set; }
    }
}
