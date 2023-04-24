namespace TurismoMongoDB.Models
{
    public class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public Endereco endereco { get; set; }
    }
}
