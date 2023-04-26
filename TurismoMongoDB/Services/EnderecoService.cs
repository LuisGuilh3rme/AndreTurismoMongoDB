using MongoDB.Bson;
using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class EnderecoService
    {
        private readonly IMongoCollection<Endereco> _collection;

        public EnderecoService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Endereco>(settings.EnderecoCollection);
        }

        public List<Endereco> Get() => _collection.Find(c => true).ToList();
        public Endereco Get(string id)
        {
            if (id == "" || id == "string") return null;
            return _collection.Find(c => c.Id == id).FirstOrDefault();
        }

        public Endereco Post(Endereco endereco)
        {
            if (endereco.Id == "" || endereco.Id == "string") endereco.Id = BsonObjectId.GenerateNewId().ToString();

            Endereco enderecoExistente = _collection.Find(e => e.Id == endereco.Id).FirstOrDefault();
            if (enderecoExistente != null)
            {
                endereco = enderecoExistente;
                return endereco;
            }

            _collection.InsertOne(endereco);
            return endereco;
        }

        public Endereco Update(Endereco endereco)
        {
            Endereco enderecoExistente = _collection.Find(e => e.Id == endereco.Id).FirstOrDefault();

            if (endereco.Logradouro == "string") endereco.Logradouro = enderecoExistente.Logradouro;
            if (endereco.Bairro == "string") endereco.Bairro = enderecoExistente.Bairro;
            if (endereco.Numero == 0) endereco.Numero = enderecoExistente.Numero;
            if (endereco.cidade.Id == "string") endereco.cidade.Id = enderecoExistente.cidade.Id;

            _collection.ReplaceOne(e => e.Id == e.Id, endereco);
            return endereco;
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(c => c.Id == id);
        }
    }
}
