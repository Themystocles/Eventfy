using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.EventParticipantServicesTest
{
    
    public class GetEventsAndParticipantsTest
    {
        private readonly Mock<IEventParticipantPersist> _eventParticipantPersistMock;
        private readonly Mock<IEventPersist> _eventPersistMock;
        private readonly Mock<IParticipantPersist> _participantPersistMock;
        private readonly EventParticipantServices _service;

        public GetEventsAndParticipantsTest()
        {
            _eventParticipantPersistMock = new Mock<IEventParticipantPersist>();
            _eventPersistMock = new Mock<IEventPersist>();
            _participantPersistMock = new Mock<IParticipantPersist>();

            _service = new EventParticipantServices(
                _eventParticipantPersistMock.Object,
                _eventPersistMock.Object,
                _participantPersistMock.Object
            );
        }
        [Fact]
        
        public async Task GetListParticipantByEventId_EventExists_ReturnsParticipants()
        {
            // Arrange
            int eventId = 1;
            var mockEvent = new Event { Id = eventId, Name = "Mock Event" };
            var participants = new List<Participant>
        {
            new Participant { Id = 1, Name = "Participant 1" },
            new Participant { Id = 2, Name = "Participant 2" }
        };

            _eventPersistMock
                .Setup(ep => ep.GetEventByIdAsync(eventId))
                .ReturnsAsync(mockEvent);

            _eventParticipantPersistMock
                .Setup(ep => ep.GetParticipantsToEventAsync(eventId))
                .ReturnsAsync(participants);

            // Act
            var result = await _service.GetListParticipantByEventId(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(participants.Count, result.Count());
            Assert.Contains(result, p => p.Name == "Participant 1");
            Assert.Contains(result, p => p.Name == "Participant 2");
        }
        [Fact]
        public async Task GetListParticipantByEventId_EventDoesNotExist_ThrowsException()
        {
            // Arrange
            int eventId = 99;

            _eventPersistMock
                .Setup(ep => ep.GetEventByIdAsync(eventId))
                .ReturnsAsync((Event)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.GetListParticipantByEventId(eventId)
            );
        }
    }

}

