using Eventfy.Interface;
using Eventfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventfy.Service
{
    public class EventService 
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

    }
}
