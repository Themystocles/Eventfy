using Eventfy.Models;
using Microsoft.AspNetCore.SignalR;

namespace Eventfy.Interface
{
    public interface IEventPersist
    {
        Task<IEnumerable<Event>> GetAllEventAsync();
        Task<Event> GetEventByIdAsync(int Id);
        Task<Event> UpdateEventAsync(Event updateEvent);
        Task<Event> DeleteEventAsync(int id);

    }

    }