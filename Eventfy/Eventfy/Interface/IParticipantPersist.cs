using Eventfy.Models;

namespace Eventfy.Interface
{
    public interface IParticipantPersist
    {
        Task <IEnumerable<Participant>> GetAllParticipantAsync();
        Task<Participant> GetParticipantByIdAsync(int id);
        Task<Participant> CreateParticipantAsync(Participant participant);
        Task<Participant> UpdateParticipantAsync(Participant participant);
        Task<Participant> DeleteParticipantAsync(Participant participant);
    }
}
