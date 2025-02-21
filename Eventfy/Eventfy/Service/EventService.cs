using Eventfy.Interface;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eventfy.Service
{
    public class EventService : IEventServices
    {
        private readonly IEventPersist _eventPersist;

        public EventService(IEventPersist eventPersist)
        {
            _eventPersist = eventPersist;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _eventPersist.GetAllEventAsync();
        }
        public async Task<Event> GetEventById(int Id)
        {
            var @event = await _eventPersist.GetEventByIdAsync(Id);
            if (@event == null) return null;
            return @event;
        }
        public async Task<Event> CreateEvent(EventDto eventDto)
        {
            if(eventDto == null){

                throw new ArgumentNullException(nameof(eventDto), "O evento não pode ser nulo.");
            }
            var @event = new Event()
            {
                Name = eventDto.Name,
                Description = eventDto.Description,
                DateEvent = eventDto.DateEvent,
                LocalId = eventDto.LocalId,
                
            };

            await _eventPersist.CreateEvent(@event);

            return @event;
        }
        public async Task<Event> UpdateEvent(EventDto Updatevent)
        {
            if (Updatevent == null)
                throw new ArgumentNullException(nameof(Updatevent), "O evento não pode ser nulo.");
            
            var existingEvent = await _eventPersist.GetEventByIdAsync(Updatevent.Id);

            if (existingEvent == null)
                throw new KeyNotFoundException($"Evento com ID {Updatevent.Id} não encontrado.");
            

            existingEvent.Name = Updatevent.Name;
            existingEvent.Description = Updatevent.Description;
            existingEvent.DateEvent = Updatevent.DateEvent;
            existingEvent.LocalId = Updatevent.LocalId;
           

             await _eventPersist.UpdateEventAsync(existingEvent);
            return existingEvent;
           
        }
        public async Task<bool> DeleteEvent(int id)
        {
           var @event = await _eventPersist.GetEventByIdAsync(id);
              if (@event == null)
             {
                 throw new ArgumentNullException(nameof(@event), "O evento não foi encontrado");
              }
            await _eventPersist.DeleteEventAsync(id);
            return true;

            

        }

    }
}
