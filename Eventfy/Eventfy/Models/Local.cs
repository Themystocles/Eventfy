﻿namespace Eventfy.Models
{
    public class Local
    {
     
        public int Id { get; set; }
        public string Endereco { get; set; }
        public int Capacidade { get; set; }
        public int IdEvent { get; set; }
        public Event Event { get; set; }

        public Local() { }

        public Local(int id, string endereco, int capacidade, Event @event, int idEvent)
        {
            Id = id;
            Endereco = endereco;
            Capacidade = capacidade;
            Event = @event;
            IdEvent = idEvent;
        }
    }
}
