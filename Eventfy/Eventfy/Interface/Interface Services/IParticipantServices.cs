using Eventfy.Models;
using Eventfy.Models.DTOs;

namespace Eventfy.Interface
{
    public interface IParticipantService
    {
        Task<IEnumerable<Participant>> GetAllParticipants();
        Task<Participant> GetParticipantByIdAsync(int id);
        Task<Participant> CreateParticipantAsync(ParticipantDto participantDto);
        Task<Participant> UpdateParticipantAsync(ParticipantDto participantDto);
        Task<bool> DeleteParticipantAsync(int id);
    }
}
