﻿using Eventfy.Data;
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
        public async Task<Local> CreateLocalAsync(Local local)
        {
          var newlocal = await _context.AddAsync(local);
          _context.SaveChangesAsync();
            return local;
        }

        public async Task<Local> DeleteLocalAsync(Local local)
        {
             _context.Locals.Remove(local);
            await _context.SaveChangesAsync();
            return local;


        }

        public async Task<IEnumerable<Local>> GetAllLocalAsync()
        {
            return await _context.Locals.ToListAsync();
        }

        public async Task<Local> GetLocalByIdAsync(int id)
        {
            var local = await _context.Locals.FindAsync(id);
            return local;
        }

        public async Task<Local> UpdateLocalAsync(Local local)
        {
             _context.Update(local);
            await _context.SaveChangesAsync();
            return local;
        }
    }
}
