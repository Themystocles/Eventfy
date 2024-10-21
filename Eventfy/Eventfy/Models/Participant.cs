namespace Eventfy.Models
{
    public class Participant
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<EventParticipant>? EventsParticipant { get; set; } = new List<EventParticipant>();

        public Participant(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            
            
        }

    }
}
