namespace Eventfy.Models
{
    public class Local
    {
     
        public int Id { get; set; }
        public string Endereco { get; set; }
        public int Capacidade { get; set; }
        public List<Event> Events { get; set; }
        public Local() { }

        public Local(int id, string endereco, int capacidade)
        {
            Id = id;
            Endereco = endereco;
            Capacidade = capacidade;
           
        }
    }
}
