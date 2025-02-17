using Eventfy.Models;
using Eventfy.Models.DTOs;
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
            var @event = await _eventService.GetEvents();

            return Ok(@event);
        }
        [HttpGet("Event/{Id}")]
        public async Task<ActionResult<Event>> GetEventById(int Id)
        {
            var @event = await _eventService.GetEventById(Id);

            return Ok(@event);
        }
        [HttpPost("CreateEvent")]
        public async Task<ActionResult<Event>> PostEvent([FromBody] EventDto eventDto)
        {
            await _eventService.CreateEvent(eventDto);

            return Ok(eventDto);
        }
        [HttpPut("EditEvent/{id}")]
        public async Task<ActionResult<Event>> PutEvent(int id, [FromBody] EventDto @event)
        {
            try
            {
                var eventDto = new EventDto()
                {   
                    Id = id,
                    Name = @event.Name,
                    Description = @event.Description,
                    DateEvent = @event.DateEvent, 
                    LocalId = @event.LocalId 
                };

                await _eventService.UpdateEvent(eventDto);

                return Ok(@event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro Interno: {ex.Message}");
            }
            
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            try
            {
                await _eventService.DeleteEvent(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); 
            }
           
        }
            
    }
}
