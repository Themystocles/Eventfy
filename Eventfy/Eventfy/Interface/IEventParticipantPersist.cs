using Eventfy.Models;

namespace Eventfy.Interface
{
    public interface IEventParticipantPersist
    {
        Task<IEnumerable<Event>> GetEventByParticipatAsync(int ParticipantId);
        Task <IEnumerable<Participant>> GetParticipantByEventAsync(int EventId);
        Task AddParticipantToEventAsync(int eventId, int participantId);
        Task RemoveParticipantFromEventAsync(int eventId, int participantId); 
    }
}
