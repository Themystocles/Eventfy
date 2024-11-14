using Eventfy.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Eventfy.Tests.Models
{
    public class EventParticipantTest
    {
        [Fact]
        public void CreateEventParticipant_Success()
        {
            // Arrange
            var local = new Local { Id = 1, Capacidade = 300, Endereco = "Av Expedicionarios 1212", };
            var event1 = new Event(1, "Palestra Pablo Marçal", "uma descrição", DateTime.Now, 0, local);
            var event2 = new Event(2, "Guanabara MySql", "Outra Descrição", DateTime.Now.AddDays(1), 0, local);

            var participant1 = new Participant(1, "John Doe", "john@example.com");
            var participant2 = new Participant(2, "Jane Doe", "jane@example.com");

            var eventParticipants = new List<EventParticipant>
            {
                new EventParticipant { IdEvent = 1, IdParticipant = 1, Event = event1, Participant = participant1 },
                new EventParticipant { IdEvent = 2, IdParticipant = 1, Event = event2, Participant = participant1 },
                new EventParticipant { IdEvent = 1, IdParticipant = 2, Event = event1, Participant = participant2 }
            };

            // Act
            var eventParticipant1 = eventParticipants[0];
            var eventParticipant2 = eventParticipants[1];
            var eventParticipant3 = eventParticipants[2];

            // Assert:
            Assert.Equal(1, eventParticipant1.IdEvent);
            Assert.Equal(1, eventParticipant1.IdParticipant);
            Assert.Equal("Palestra Pablo Marçal", eventParticipant1.Event.Name);
            Assert.Equal("John Doe", eventParticipant1.Participant.Name);

            Assert.Equal(2, eventParticipant2.IdEvent);
            Assert.Equal(1, eventParticipant2.IdParticipant);
            Assert.Equal("Guanabara MySql", eventParticipant2.Event.Name);
            Assert.Equal("John Doe", eventParticipant2.Participant.Name);

            Assert.Equal(1, eventParticipant3.IdEvent);
            Assert.Equal(2, eventParticipant3.IdParticipant);
            Assert.Equal("Palestra Pablo Marçal", eventParticipant3.Event.Name);
            Assert.Equal("Jane Doe", eventParticipant3.Participant.Name);
        }
    }
}
