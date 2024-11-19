using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;

namespace Eventfy.Service
{
    public class LocalService
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

    }
}
