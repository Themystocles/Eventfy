using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace Eventfy.Service
{
    public class EventParticipantServices
    {
        private readonly IEventParticipantPersist _eventParticipant;
        private readonly IEventPersist _eventPersist;
        private readonly IParticipantPersist _participantPersist;

        public EventParticipantServices(IEventParticipantPersist eventParticipant, IEventPersist eventpersist, IParticipantPersist participantPersist)
        {
            _eventParticipant = eventParticipant;
            _eventPersist = eventpersist;
            _participantPersist = participantPersist;

        }
        public async Task<EventParticipant> AddEventParticipant(EventParticipantDto eventParticipantDto)
        {
            var eventExistent = await _eventPersist.GetEventByIdAsync(eventParticipantDto.EventId);

            if (eventExistent == null)
            {
                throw new ArgumentNullException(nameof(eventParticipantDto), "O Evento não foi encontrado.");

            }
            var ParticipantExistent = await _participantPersist.GetParticipantByIdAsync(eventParticipantDto.ParticipantId);
            if (ParticipantExistent == null)
            {
                throw new ArgumentNullException(nameof(eventParticipantDto), "O Participante não foi encontrado");
            }

            var eventParticipant = new EventParticipant
            {
                EventId = eventParticipantDto.EventId,
                ParticipantId = eventParticipantDto.ParticipantId,
            };
            await _eventParticipant.AddParticipantToEventAsync(eventParticipant.EventId, eventParticipant.ParticipantId);

            return eventParticipant;

        }
        public async Task<EventParticipant> UpdateEventParticipantAsync(EventParticipantDto eventParticipant)
        {

            if (eventParticipant == null)
            {
                throw new ArgumentNullException(nameof(eventParticipant), "O participante do evento não pode ser nulo.");
            }
            var existingEventParticipant = await _eventParticipant.GetEventparticipantByIdAsync(eventParticipant.Id);
            if (existingEventParticipant == null)
            {
                throw new ArgumentException(nameof(existingEventParticipant.Id), "O Id do EventParticipant não foi encontrado.");
            }

            var eventExistent = await _eventPersist.GetEventByIdAsync(eventParticipant.EventId);
            if (eventExistent == null)
            {
                throw new ArgumentException(nameof(eventParticipant.EventId), "O Id do Evento não foi encontrado.");
            }
            var participantExistent = await _participantPersist.GetParticipantByIdAsync(eventParticipant.ParticipantId);
            if (participantExistent == null)
            {
                throw new ArgumentException(nameof(eventParticipant.ParticipantId), "O Id do Participante não foi encontrado.");
            }
            existingEventParticipant.EventId = eventParticipant.EventId;
            existingEventParticipant.ParticipantId = eventParticipant.ParticipantId;
            await _eventParticipant.UpdateParticipantToEventAsync(existingEventParticipant);
            return existingEventParticipant;
        }

        public async Task<IEnumerable<Participant>> GetListParticipantByEventId(int eventId)
        {
            var eventExist = await _eventPersist.GetEventByIdAsync(eventId);

            if (eventExist == null)
            {
                throw new ArgumentException("O evento com o ID fornecido não existe.", nameof(eventId));
            }
            var participant = await _eventParticipant.GetParticipantsToEventAsync(eventId);

            return participant;

        }
        public async Task<IEnumerable<Event>> GetListEventByParticipantId(int participantId)
        {
            var participantExist = await _participantPersist.GetParticipantByIdAsync(participantId);
            if (participantExist == null)
            {
                throw new ArgumentException("O Participant com o ID fornecido não existe.", nameof(participantId));
            }
            var @event = await _eventParticipant.GetEventToParticipantAsync(participantId);

            return @event;
        }

    }
}

