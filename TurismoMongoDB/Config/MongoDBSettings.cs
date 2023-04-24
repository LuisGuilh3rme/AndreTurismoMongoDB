namespace TurismoMongoDB.Config
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CidadeCollection { get; set; }
        public string EnderecoCollection { get; set; }
        public string ClienteCollection { get; set; }
    }
}
