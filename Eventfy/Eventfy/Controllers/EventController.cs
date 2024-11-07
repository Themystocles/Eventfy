using Eventfy.Models;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;

namespace Eventfy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
      private readonly EventService _eventService;
        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet("Event")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventsAssync()
        {
            var @event = await  _eventService.GetEvents();

            return  @event.ToList();   
        }
    }
}
