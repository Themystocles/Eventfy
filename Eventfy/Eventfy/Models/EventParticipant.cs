namespace Eventfy.Models
{
    public class EventParticipant
    {
        public int Id { get; set; }
        public int IdEvent { get; set; }
        public Event Event { get; set; }
        public int IdParticipant { get; set; }
        public Participant Participant { get; set; }
    }
}
