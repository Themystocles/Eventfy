using Eventfy.Models.DTOs;
using Eventfy.Models;

namespace Eventfy.Interface.Interface_Services
{
    public interface IEventParticipantServices
    {

        Task<EventParticipant> AddEventParticipant(EventParticipantDto eventParticipantDto);
        Task<EventParticipant> UpdateEventParticipantAsync(EventParticipantDto eventParticipant);
        Task<IEnumerable<Participant>> GetListParticipantByEventId(int eventId);
        Task<IEnumerable<Event>> GetListEventByParticipantId(int participantId);
        Task RemoveEventParticipant(int eventId, int participantId);
    }
}
