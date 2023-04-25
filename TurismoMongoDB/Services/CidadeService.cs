using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using TurismoMongoDB.Config;
using TurismoMongoDB.Models;

namespace TurismoMongoDB.Services
{
    public class CidadeService
    {
        private readonly IMongoCollection<Cidade> _collection;

        public CidadeService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<Cidade>(settings.CidadeCollection);
        }

        public List<Cidade> Get() => _collection.Find(c => true).ToList();
        public Cidade Get(string id) => _collection.Find(c => c.Id == id).FirstOrDefault();

        public Cidade Post(Cidade cidade)
        {
            if (cidade.Id == "" || cidade.Id == "string") cidade.Id = BsonObjectId.GenerateNewId().ToString();

            Cidade cidadeExistente = _collection.Find(c => c.Id == cidade.Id).FirstOrDefault();
            if (cidadeExistente != null)
            {
                cidade = cidadeExistente;
                return cidade;
            }

            _collection.InsertOne(cidade);
            return cidade;
        }

        public Cidade Update(Cidade cidade)
        {
            _collection.ReplaceOne(c => c.Id == cidade.Id, cidade);
            return cidade;
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(c => c.Id == id);
        }
    }
}
