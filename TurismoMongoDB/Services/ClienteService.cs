using MongoDB.Bson;
using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Cliente> _collection;

        public ClienteService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Cliente>(settings.ClienteCollection);
        }

        public List<Cliente> Get() => _collection.Find(c => true).ToList();
        public Cliente Get(string id)
        {
            if (id == "" || id == "string") return null;
            return _collection.Find(c => c.Id == id).FirstOrDefault();
        }


            public Cliente Post(Cliente cliente)
        {
            if (cliente.Id == "" || cliente.Id == "string") cliente.Id = BsonObjectId.GenerateNewId().ToString();

            Cliente clienteExistente = _collection.Find(c => c.Id == cliente.Id).FirstOrDefault();
            if (clienteExistente != null)
            {
                cliente = clienteExistente;
                return cliente;
            }

            _collection.InsertOne(cliente);
            return cliente;
        }

        public Cliente Update(Cliente cliente)
        {
            Cliente clienteExistente = _collection.Find(c => c.Id == cliente.Id).FirstOrDefault();

            if (cliente.Nome == "string") cliente.Nome = clienteExistente.Nome;
            if (cliente.CPF == "string") cliente.CPF = clienteExistente.CPF;
            if (cliente.endereco.Id == "string") cliente.endereco = clienteExistente.endereco;

            _collection.ReplaceOne(c => c.Id == cliente.Id, cliente);
            return cliente;
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(c => c.Id == id);
        }
    }
}
