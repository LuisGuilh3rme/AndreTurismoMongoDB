namespace TurismoMongoDB.Config
{
    public interface IMongoDBSettings
    {
        string CidadeCollection { get; set; }
        string EnderecoCollection { get; set; }
        string ClienteCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
