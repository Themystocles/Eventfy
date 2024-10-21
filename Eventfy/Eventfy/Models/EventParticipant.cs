namespace Eventfy.Models
{
    public class EventParticipant
    {
        public int IdEvent { get; set; }
        public Event Event { get; set; }
        public int IdParticipant { get; set; }
        public Participant Participant { get; set; }
    }
}
