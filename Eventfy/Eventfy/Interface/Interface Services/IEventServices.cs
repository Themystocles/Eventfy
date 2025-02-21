using Eventfy.Models.DTOs;
using Eventfy.Models;

namespace Eventfy.Interface.Interface_Services
{
    public interface IEventServices
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEventById(int Id);
        Task<Event> CreateEvent(EventDto eventDto);
        Task<Event> UpdateEvent(EventDto Updatevent);
        Task<bool> DeleteEvent(int id);
    }
}
