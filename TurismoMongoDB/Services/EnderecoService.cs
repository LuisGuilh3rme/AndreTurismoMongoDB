using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class EnderecoService
    {
        private readonly IMongoCollection<Endereco>? _endereco;

        public EnderecoService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _endereco = database.GetCollection<Endereco>("endereco");
        }

        public List<Endereco> Get() => _endereco.Find(c => true).ToList();
        public Endereco Get(string id) => _endereco.Find(c => c.Id == id).FirstOrDefault();

        public void Post(Endereco endereco)
        {
            _endereco.InsertOne(endereco);
        }

        public Endereco Update(Endereco endereco)
        {
            _endereco.ReplaceOne(e => e.Id == e.Id, endereco);
            return endereco;
        }

        public void Delete(string id)
        {
            _endereco.DeleteOne(c => c.Id == id);
        }
    }
}
