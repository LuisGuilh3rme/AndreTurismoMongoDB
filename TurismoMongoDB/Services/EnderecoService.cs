using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class EnderecoService
    {
        private readonly Context? _context = new();

        public EnderecoService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _context.Endereco = database.GetCollection<Endereco>("endereco");
            _context.Cidade = database.GetCollection<Cidade>("cidade");
        }

        public List<Endereco> Get() => _context.Endereco.Find(c => true).ToList();
        public Endereco Get(string id) => _context.Endereco.Find(c => c.Id == id).FirstOrDefault();

        public void Post(Endereco endereco)
        {
            var cidade = _context.Cidade.Find(c => c.Id == endereco.cidade.Id).FirstOrDefault();
            if (cidade == null) _context.Cidade.InsertOne(endereco.cidade);
            else endereco.cidade = cidade;

            _context.Endereco.InsertOne(endereco);
        }

        public Endereco Update(Endereco endereco)
        {
            _context.Endereco.ReplaceOne(e => e.Id == e.Id, endereco);
            return endereco;
        }

        public void Delete(string id)
        {
            _context.Endereco.DeleteOne(c => c.Id == id);
        }
    }
}
