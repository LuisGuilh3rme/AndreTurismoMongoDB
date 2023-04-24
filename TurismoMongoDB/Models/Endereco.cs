namespace TurismoMongoDB.Models
{
    public class Endereco
    {
        public string Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public Cidade cidade { get; set; }
    }
}
