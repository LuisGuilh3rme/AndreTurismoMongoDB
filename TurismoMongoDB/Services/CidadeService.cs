using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class CidadeService
    {
        private readonly Context? _context = new();

        public CidadeService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _context.Cidade = database.GetCollection<Cidade>(settings.CidadeCollection);
        }

        public List<Cidade> Get() => _context.Cidade.Find(c => true).ToList();
        public Cidade Get(string id) => _context.Cidade.Find(c => c.Id == id).FirstOrDefault();

        public void Post (Cidade cidade)
        {
            _context.Cidade.InsertOne(cidade);
        }

        public Cidade Update (Cidade cidade)
        {
            _context.Cidade.ReplaceOne(c => c.Id == cidade.Id, cidade);
            return cidade;
        }

        public void Delete (string id)
        {
            _context.Cidade.DeleteOne(c => c.Id == id);
        }
    }
}
