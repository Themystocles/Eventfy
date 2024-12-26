using Eventfy.Models;

namespace Eventfy.Interface
{
    public interface IEventParticipantPersist
    {
        Task <IEnumerable<Participant>> GetParticipantsToEventAsync(int eventId);
        Task <IEnumerable<Event>> GetEventToParticipantAsync(int participantId);
        Task AddParticipantToEventAsync(int eventId, int participantId);
        Task RemoveParticipantFromEventAsync(int eventId, int participantId); 
    }
}
