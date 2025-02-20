using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Persistence;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Eventfy.Service
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantPersist _participantPersist;
        public ParticipantService(IParticipantPersist participantPersist)
        {
            _participantPersist = participantPersist;   
            
        }

     public async Task <IEnumerable<Participant>> GetAllParticipants()
        {
            var participants = await _participantPersist.GetAllParticipantAsync();
            return participants ?? Enumerable.Empty<Participant>();
           

        }
        public async Task<Participant> GetParticipantByIdAsync(int id)
        {
            var participant = await _participantPersist.GetParticipantByIdAsync(id);
            if (participant == null)
            {
                throw new KeyNotFoundException($"Participant with ID {id} not found.");
            }
            return participant;
        }
        public async Task<Participant> CreateParticipantAsync(ParticipantDto participantDto) 
        {
            if (participantDto == null)
            {
                throw new ArgumentNullException(nameof(participantDto), "O objeto participant não pode ser nulo ");
            }
            var participant = new Participant()
            {
                Id = participantDto.Id,
                Email = participantDto.Email,
                Name = participantDto.Name,
            };
            await _participantPersist.CreateParticipantAsync(participant);
            return participant;
        }
        public async Task<Participant> UpdateParticipantAsync(ParticipantDto participantDto)
        {
            if (participantDto == null)
            {
                throw new ArgumentNullException(nameof(participantDto), "O objeto participantDto não pode ser nulo ");
            }
            var ParticipantExistent = await _participantPersist.GetParticipantByIdAsync(participantDto.Id);
            if (ParticipantExistent == null)
            {
                throw new KeyNotFoundException($"O participante com o ID {participantDto.Id} não foi encontrado.");
            }
            ParticipantExistent.Email = participantDto.Email;
            ParticipantExistent.Name = participantDto.Name;

            await _participantPersist.UpdateParticipantAsync(ParticipantExistent);
            return ParticipantExistent;
        }
        public async Task<bool> DeleteParticipantAsync(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("O Id passado não é válido.", nameof(Id));
            }
            var participantexistent = await GetParticipantByIdAsync(Id);
            if (participantexistent == null)
            {
                throw new KeyNotFoundException($"O participante com ID {Id} não foi encontrado.");
            }
            await _participantPersist.DeleteParticipantAsync(participantexistent);
            return true;

        }
    }
}
