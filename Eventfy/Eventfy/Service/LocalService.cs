using Eventfy.Interface;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventfy.Service
{
    public class LocalService : ILocalServices

    {
        private readonly ILocalPersist _localPersist;

        public LocalService(ILocalPersist localPersist)
        {       
            _localPersist = localPersist;
            
        }
        public async Task <IEnumerable<Local>> GetAllLocalsAsync()
        {
            var locals = await _localPersist.GetAllLocalAsync();
            if (locals == null)
            {
                throw new ArgumentNullException(nameof(locals));
            }
            return locals;
        }
        public async Task <Local> GetLocalByIdAsync(int id)
        {
            var local = await _localPersist.GetLocalByIdAsync(id);
            if (local == null) 
            { 
                throw new ArgumentNullException(nameof(local), "O id passado não existe"); 
            }
            return local;

        }
        public async Task <Local> CreateLocalAsync(LocalDto localDto)
        {
            if (localDto == null)
            {
                throw new ArgumentNullException(nameof(localDto), "Local não pode ser nulo");
            }
            var local = new Local()
            { 
                Id = localDto.Id,
                Capacidade = localDto.Capacidade,
                Endereco = localDto.Endereco,
            };
             await _localPersist.CreateLocalAsync(local);

            return local;


        }
        public async Task<Local> UpdateLocalAsync(LocalDto localdto)
        {
            if (localdto == null)
            {
                throw new ArgumentNullException(nameof(localdto), "O parâmetro 'localdto' não pode ser nulo.");
            }
            var localExistente = await _localPersist.GetLocalByIdAsync(localdto.Id);
          
            if (localExistente == null)
            {
                throw new ArgumentNullException(nameof(localExistente), "O Local não foi encontrado.");
            }

            localExistente.Endereco = localdto.Endereco;
            localExistente.Endereco = localdto.Endereco;
            localExistente.Capacidade = localdto.Capacidade;


            await _localPersist.UpdateLocalAsync(localExistente);
            return localExistente;

        }
        public async Task<bool> DeleteLocal(int id)
        {
            var local = await _localPersist.GetLocalByIdAsync(id);
            if (local == null) 
            {
                throw new ArgumentNullException(nameof(local));
            }
            await _localPersist.DeleteLocalAsync(local);

            return true;
           
           
        }

    }
}
