using Eventfy.Models;
using Microsoft.AspNetCore.Http.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Models
{
    public class ParticipantTest
    {
        [Fact]
        public void CreateParticipantSuccess()
        {
            //Arrange
            var name = "Themystocles";
            var email = "Themystocles@outlook.com";
            var local = new Local { Id = 1, Capacidade = 300, Endereco = "Av Expedicionarios 1212", };
            var events = new List<EventParticipant>
            { 
                new EventParticipant { EventId = 1, ParticipantId = 1, Event = new Event(1,  "Palestra Pablo Marçal",  "uma descrição", DateTime.Now, 0 , local ) },
                new EventParticipant { EventId = 2, ParticipantId = 1, Event = new Event(2,  "Guanabara MySql",  "Outra Descição", DateTime.Now,  0 , local ) }
            };


            //Act
            var participant = new Participant(0, name, email);
            participant.EventsParticipant = events;

            //Assert
            Assert.Equal( name, participant.Name );
            Assert.Equal(email, participant.Email );
            Assert.Equal(2, participant.EventsParticipant.Count);
            Assert.Equal("Palestra Pablo Marçal", participant.EventsParticipant[0].Event.Name);
            Assert.Equal("Guanabara MySql", participant.EventsParticipant[1].Event.Name);




        }
    }
}
