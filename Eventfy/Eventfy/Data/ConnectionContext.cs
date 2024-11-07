using Eventfy.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventfy.Data
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Event> Events {get; set;}
        public DbSet<Local> Locals { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }



    }
}
