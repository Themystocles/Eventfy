using Eventfy.Models;

namespace Eventfy.Interface
{
    public interface IEventParticipantPersist
    {
        Task <EventParticipant> GetEventparticipantByIdAsync (int id);
        Task <IEnumerable<Participant>> GetParticipantsToEventAsync(int eventId);
        Task <IEnumerable<Event>> GetEventToParticipantAsync(int participantId);
        Task<EventParticipant> GetEventParticipantAsync(int eventId, int participantId);
        Task <EventParticipant> AddParticipantToEventAsync(int eventId, int participantId);
        Task UpdateParticipantToEventAsync(EventParticipant Updateventparticipant);
        Task RemoveParticipantFromEventAsync(int eventId, int participantId); 
    }
}
