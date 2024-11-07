using Eventfy.Data;
using Eventfy.Interface;
using Eventfy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventfy.Persistence

{
    public class EventPersist : IEventPersist
    {
        private readonly ConnectionContext _context;

        public EventPersist(ConnectionContext context)
        {
            _context = context;
            
        }
        public Task<Event> DeleteEventAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Event>> GetAllEventAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int Id)
        {
            var @event = await _context.FindAsync<Event>(Id);
            

            return @event;
        }

        public Task<Event> UpdateEventAsync(Event updateEvent)
        {
            throw new NotImplementedException();
        }
    }
}
