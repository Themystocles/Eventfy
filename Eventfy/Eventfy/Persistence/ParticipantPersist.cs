using Eventfy.Data;
using Eventfy.Interface;
using Eventfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventfy.Persistence
{
    public class ParticipantPersist : IParticipantPersist
    {
        private readonly ConnectionContext _connectioncontext;
        public ParticipantPersist(ConnectionContext context)
        {
            _connectioncontext = context;
            
        }
        public async Task<Participant> CreateParticipantAsync(Participant participant)
        {
            var Newparticpant = await _connectioncontext.AddAsync(participant);
            await _connectioncontext.SaveChangesAsync();
            return participant;
        }

        public async Task<Participant> DeleteParticipantAsync(Participant participant)
        {
             _connectioncontext.Remove(participant); 
            await _connectioncontext.SaveChangesAsync();
            return participant;
        }

        public async Task<IEnumerable<Participant>> GetAllParticipantAsync()
        {
            var participants = await _connectioncontext.Participants.ToListAsync();
            return participants;
        }

        public async Task<Participant> GetParticipantByIdAsync(int id)
        {
            var participant = await _connectioncontext.Participants.FindAsync(id);
           
            return participant;
        }

        public async Task<Participant> UpdateParticipantAsync(Participant participant)
        {
            _connectioncontext.Participants.Update(participant);
            await _connectioncontext.SaveChangesAsync();
            return participant;
        }
    }
}
