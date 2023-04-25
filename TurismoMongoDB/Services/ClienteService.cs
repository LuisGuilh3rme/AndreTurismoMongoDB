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
        public Cliente Get(string id) => _collection.Find(c => c.Id == id).FirstOrDefault();

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
            _collection.ReplaceOne(c => c.Id == cliente.Id, cliente);
            return cliente;
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(c => c.Id == id);
        }
    }
}
