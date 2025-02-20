using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;

namespace Eventfy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participanteService;
        public ParticipantController(IParticipantService participantService)
        {
            _participanteService = participantService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetAllParticipant()
            {
            var participants = await _participanteService.GetAllParticipants();

            return Ok(participants);
            }
        
        [HttpGet("Participant/{Id}")]
        public async Task<Participant> GetParticipantById(int Id)
        {
            try
            {
                return await _participanteService.GetParticipantByIdAsync(Id);

            }
            catch (KeyNotFoundException ex)
            {
                return null;
            }
           

        }
        [HttpPost("Adicionar/Participant")]
        public async Task <ActionResult<Participant>> AdicionarParticipantAsync([FromBody] ParticipantDto participantdto)
        {
            try
            {
                await _participanteService.CreateParticipantAsync(participantdto);
                return Ok(participantdto);
            }catch
            (ArgumentNullException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
        [HttpPut("Editar/Participant/{Id}")]
        public async Task <ActionResult<Participant>> UpdateParticipantAsync(int Id, [FromBody] ParticipantDto participantdto)
        {
            try
            {
                var participantDto = new ParticipantDto() { Id = Id, Name = participantdto.Name, Email = participantdto.Email };  
                await _participanteService.UpdateParticipantAsync(participantDto);
                return Ok(participantdto);
            }
            catch (ArgumentNullException ex) 
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Captura qualquer outro erro inesperado
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        [HttpDelete("Deletar/Participant/{Id}")]
        public async Task<ActionResult<Participant>> DeleteParticipatAsync(int Id)
        {
            try
            {
                await _participanteService.DeleteParticipantAsync(Id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { error = "Ocorreu um erro inesperado.", details = ex.Message });
            }
        }



    }
}
