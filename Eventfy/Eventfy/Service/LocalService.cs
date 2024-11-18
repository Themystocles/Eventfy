using Eventfy.Interface;
using Eventfy.Models;

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
    }
}
