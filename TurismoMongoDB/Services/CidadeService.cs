using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class CidadeService
    {
        private readonly IMongoCollection<Cidade>? _cidade;

        public CidadeService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cidade = database.GetCollection<Cidade>("cidade");
        }

        public List<Cidade> Get() => _cidade.Find(c => true).ToList();
        public Cidade Get(string id) => _cidade.Find(c => c.Id == id).FirstOrDefault();

        public Cidade Post (Cidade cidade)
        {
            _cidade.InsertOne(cidade);
            return cidade;
        }

        public Cidade Update (Cidade cidade)
        {
            _cidade.ReplaceOne(c => c.Id == cidade.Id, cidade);
            return cidade;
        }

        public void Delete (string id)
        {
            _cidade.DeleteOne(c => c.Id == id);
        }
    }
}
