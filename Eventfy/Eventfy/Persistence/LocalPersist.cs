using Eventfy.Data;
using Eventfy.Interface;
using Eventfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventfy.Persistence
{
    public class LocalPersist : ILocalPersist
    {
        private readonly ConnectionContext _context;

        public LocalPersist(ConnectionContext context)
        {   
            _context = context;
            
        }
        public Task<Local> CreateLocalAsync(Local local)
        {
            throw new NotImplementedException();
        }

        public Task<Local> DeleteLocalAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Local>> GetAllLocalAsync()
        {
            return await _context.Locals.ToListAsync();
        }

        public Task<Local> GetLocalByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Local> UpdateLocalAsync(Local local)
        {
            throw new NotImplementedException();
        }
    }
}
