using Eventfy.Data;
using Eventfy.Interface;
using Eventfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventfy.Persistence
{
    public class EventParticipantPersist : IEventParticipantPersist
    {
        private readonly ConnectionContext _connectionContext;
        
        public EventParticipantPersist(ConnectionContext connectionContext)
        {   
            _connectionContext = connectionContext;
        }
        public async Task AddParticipantToEventAsync(int eventId, int participantId)
        {
            var eventPartipant = new EventParticipant
            {
                EventId = eventId,
                ParticipantId = participantId
            };
            _connectionContext.EventParticipants.Add(eventPartipant);

            await _connectionContext.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Event>> GetEventToParticipantAsync(int participantId)
        {
            var events = await _connectionContext.EventParticipants
                .Where(ep => ep.ParticipantId == participantId)
                .Select(ep => ep.Event)
                .ToListAsync();
            return events;
        }

        public async Task<IEnumerable<Participant>> GetParticipantsToEventAsync(int eventId)
        {
            var participants = await _connectionContext.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .Select(ep => ep.Participant)
                .ToListAsync();
                   
            return participants;

        }

        public Task RemoveParticipantFromEventAsync(int eventId, int participantId)
        {
            throw new NotImplementedException();
        }
    }
}
