namespace Eventfy.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateEvent { get; set; }
        public List<EventParticipant>? EventsParticipant { get; set; } = new List<EventParticipant>();

        public Event() { }
        public Event(int id, string name, string description, DateTime dateEvent)
        {
            Id = id;
            Name = name;
            Description = description;
            DateEvent = dateEvent;



        }
    }
}
