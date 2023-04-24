using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Cliente>? _cliente;

        public ClienteService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cliente = database.GetCollection<Cliente>("cliente");
        }

        public List<Cliente> Get() => _cliente.Find(c => true).ToList();
        public Cliente Get(string id) => _cliente.Find(c => c.Id == id).FirstOrDefault();

        public void Post(Cliente cliente)
        {
            _cliente.InsertOne(cliente);
        }

        public Cliente Update(Cliente cliente)
        {
            _cliente.ReplaceOne(c => c.Id == cliente.Id, cliente);
            return cliente;
        }

        public void Delete(string id)
        {
            _cliente.DeleteOne(c => c.Id == id);
        }
    }
}
