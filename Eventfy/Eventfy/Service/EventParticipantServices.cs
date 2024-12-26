﻿using Eventfy.Interface;
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

        public EventParticipantServices(IEventParticipantPersist eventParticipant, IEventPersist eventpersist,IParticipantPersist participantPersist)
        {
            _eventParticipant = eventParticipant;
            _eventPersist = eventpersist;
            _participantPersist = participantPersist;
                
        }
        public async Task<EventParticipant> AddEventParticipant(EventParticipantDto eventParticipantDto)
        {
            var eventExistent = await _eventPersist.GetEventByIdAsync(eventParticipantDto.IdEvent);

            if (eventExistent == null)
            {
                throw new ArgumentNullException(nameof(eventParticipantDto), "Os dados não podem ser nulos");
                
            }
            var ParticipantExistent = await _participantPersist.GetParticipantByIdAsync(eventParticipantDto.IdParticipant);
            if (ParticipantExistent == null)
            {
                throw new ArgumentNullException(nameof(eventParticipantDto), "Os dados não podem ser nulos");
            }

            var eventParticipant = new EventParticipant
            {
                EventId = eventParticipantDto.IdEvent,
                ParticipantId = eventParticipantDto.IdParticipant,
            };
            await _eventParticipant.AddParticipantToEventAsync(eventParticipant.EventId, eventParticipant.ParticipantId);

            return eventParticipant;

            
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

