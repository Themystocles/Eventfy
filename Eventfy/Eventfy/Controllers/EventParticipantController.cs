using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eventfy.Controllers
{
    public class EventParticipantController : ControllerBase
    {
        private readonly IEventParticipantServices _eventParticipant;
        public EventParticipantController(IEventParticipantServices eventParticipantServices)
        {
            _eventParticipant = eventParticipantServices;
        }
        [HttpPost("CreateEventParticipant/{eventId}/{participantId}")]
        public async Task <ActionResult<EventParticipantDto>> AddEventParticipant(int eventId, int participantId)
        {
            var eventParticipant = new EventParticipantDto()
            {
                EventId = eventId,
                ParticipantId = participantId
            };
            try
            {
                await _eventParticipant.AddEventParticipant(eventParticipant);
            }catch (ArgumentNullException ex) {
                return BadRequest(ex.Message);
            }
            

            return Ok(eventParticipant);

        }
        [HttpPut("UpdateEventParticipant/{EventParticipantId}")]
        public async Task<ActionResult<EventParticipant>> UpdateEventParticipant(int EventParticipantId, [FromBody] EventParticipantDto eventParticipantDto)
        {
            if (eventParticipantDto == null)
            {
                return BadRequest("O participante do evento não pode ser nulo.");
            }

           
            var eventParticipant = new EventParticipantDto()
            {
                
                Id = EventParticipantId, 
                EventId = eventParticipantDto.EventId,
                ParticipantId = eventParticipantDto.ParticipantId,
             
            };
            try
            {
                var updatedEventParticipant = await _eventParticipant.UpdateEventParticipantAsync(eventParticipant);
                return Ok(updatedEventParticipant); 
            }
            catch (ArgumentException ex)
            {
                
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }



        [HttpGet("Participants/{eventId}")]
        public async Task <ActionResult<IEnumerable<Participant>>> GetParticipantByEventId(int eventId)
        {
           var participants = await _eventParticipant.GetListParticipantByEventId(eventId);

            return Ok(participants);

        }
        [HttpGet("Event/{ParticipantId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventByParticipantId(int ParticipantId)
        {
            var @event = await _eventParticipant.GetListEventByParticipantId(ParticipantId);

            return Ok(@event);

        }
        [HttpDelete("Eventparticipant/{eventId}/{participantId}")]
        public async Task<IActionResult> DeleteEventParticipant(int eventId, int participantId)
        {
             await _eventParticipant.RemoveEventParticipant(eventId, participantId);

           

            return NoContent();
        }

    }
}
