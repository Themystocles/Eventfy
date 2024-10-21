using Eventfy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Models
{
  
   public class EventTest
    {
        [Fact]
        
        public void CreateEventsuccess()
        {
            //Arrange
            var name = "name";
            var description = "description";
            var dateEvent = DateTime.Now;
            var participants = new List<EventParticipant>
            {
                new EventParticipant { IdEvent = 1, IdParticipant = 1, Participant = new Participant(1, "John Doe", "john@example.com") },
                new EventParticipant { IdEvent = 1, IdParticipant = 2, Participant = new Participant(2, "Jane Doe", "jane@example.com") }
            };

            //Act
            var evento = new Event(0, name, description, dateEvent);
            evento.EventsParticipant = participants;

            //Assert
            Assert.Equal(evento.Name, name);
            Assert.Equal(evento.Description, description);
            Assert.Equal(evento.DateEvent, dateEvent);
            Assert.Equal(2, evento.EventsParticipant.Count);
            Assert.Equal("John Doe", evento.EventsParticipant[0].Participant.Name); 
            Assert.Equal("Jane Doe", evento.EventsParticipant[1].Participant.Name); 
        }



    }
}
