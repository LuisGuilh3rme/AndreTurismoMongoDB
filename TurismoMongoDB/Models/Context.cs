using MongoDB.Driver;

namespace TurismoMongoDB.Models
{
    public class Context
    {
        public IMongoCollection<Cidade> Cidade { get; set; }
        public IMongoCollection<Endereco> Endereco { get; set; }
        public IMongoCollection<Cliente> Cliente { get; set; }
    }
}
