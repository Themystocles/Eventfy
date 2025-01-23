using Eventfy.Data;
using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eventfy.Persistence

{
    public class EventPersist : IEventPersist
    {
        private readonly ConnectionContext _context;

        public EventPersist(ConnectionContext context)
        {
            _context = context;
            
        }

        public async Task<Event> CreateEvent(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event> DeleteEventAsync(int id)
        {
           var @event =  await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return @event;

            
        }

        public async Task<IEnumerable<Event>> GetAllEventAsync()
        {

           return  await _context.Events
                .Include(e => e.Local)
                .ToListAsync();

            
        }

        public async Task<Event?> GetEventByIdAsync(int Id)
        {
            var @event = await _context.FindAsync<Event>(Id);
            

            return @event;
        }

        public async Task<Event> UpdateEventAsync(Event updateEvent)
        {
            _context.Events.Update(updateEvent);
            await _context.SaveChangesAsync();
            return updateEvent;
        }
    }
}
