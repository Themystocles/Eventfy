using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eventfy.Models
{
    public class Event
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required] 
        public DateTime DateEvent { get; set; }

        [ForeignKey("Local")] 
        public int? LocalId { get; set; }
        
        public Local? Local { get; set; }

        
        public List<EventParticipant>? EventsParticipant { get; set; } = new List<EventParticipant>();

        // Construtor padrão
        public Event() { }

        // Construtor com parâmetros
        public Event(int id, string name, string description, DateTime dateEvent, int localId, Local local)
        {
            Id = id;
            Name = name;
            Description = description;
            DateEvent = dateEvent;
            LocalId = localId;
            Local = local;
        }
    }
}
