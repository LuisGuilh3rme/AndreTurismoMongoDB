using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class ClienteService
    {
        private readonly Context _context = new();

        public ClienteService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _context.Cliente = database.GetCollection<Cliente>(settings.ClienteCollection);
            _context.Endereco = database.GetCollection<Endereco>(settings.EnderecoCollection);
            _context.Cidade = database.GetCollection<Cidade>(settings.CidadeCollection);
        }

        public List<Cliente> Get() => _context.Cliente.Find(c => true).ToList();
        public Cliente Get(string id) => _context.Cliente.Find(c => c.Id == id).FirstOrDefault();

        public void Post(Cliente cliente)
        {
            var endereco = _context.Endereco.Find(e => e.Id == cliente.endereco.Id).FirstOrDefault();
            if (endereco == null) _context.Endereco.InsertOne(cliente.endereco);
            else cliente.endereco = endereco;

            var cidade = _context.Cidade.Find(c => c.Id == cliente.endereco.cidade.Id).FirstOrDefault();
            if (cidade == null) _context.Cidade.InsertOne(cliente.endereco.cidade);
            else cliente.endereco.cidade = cidade;

            _context.Cliente.InsertOne(cliente);
        }

        public Cliente Update(Cliente cliente)
        {
            _context.Cliente.ReplaceOne(c => c.Id == cliente.Id, cliente);
            return cliente;
        }

        public void Delete(string id)
        {
            _context.Cliente.DeleteOne(c => c.Id == id);
        }
    }
}
