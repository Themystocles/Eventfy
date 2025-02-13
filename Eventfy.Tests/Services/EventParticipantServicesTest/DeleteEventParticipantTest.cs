using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Persistence;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.EventParticipantServicesTest
{
    public class DeleteEventParticipantTest
    {
        private readonly Mock<IEventParticipantPersist> _EventParticipantPersist;
        private readonly Mock<IEventPersist> _eventPersistMock;
        private readonly Mock<IParticipantPersist> _participantPersistMock;
        private readonly EventParticipantServices _eventParticipantServices;
        public DeleteEventParticipantTest()
        {
            _EventParticipantPersist = new Mock<IEventParticipantPersist>();
            _eventPersistMock = new Mock<IEventPersist>();
            _participantPersistMock = new Mock<IParticipantPersist>();

            _eventParticipantServices = new EventParticipantServices(
                _EventParticipantPersist.Object,
                _eventPersistMock.Object,
                _participantPersistMock.Object
                );
        }
        [Fact]
        public async Task Should_Remove_Participant_When_Exists()
        {
            int eventId = 1;
            int participantId = 1;
            
            var eventparticipant = new EventParticipant() 
            {
                Id = 1,
                EventId = eventId,
                ParticipantId = participantId,
            };

            _EventParticipantPersist
              .Setup(ep => ep.GetEventParticipantAsync(eventId, participantId))
              .ReturnsAsync(eventparticipant);

            _EventParticipantPersist
                .Setup(ep => ep.RemoveParticipantFromEventAsync(
                  eventparticipant.EventId, eventparticipant.ParticipantId))
                .Returns(Task.CompletedTask);

             await _eventParticipantServices.RemoveEventParticipant(eventId, participantId);

            _EventParticipantPersist.Verify(ep => ep.RemoveParticipantFromEventAsync(eventId, participantId), Times.Once);

        }
        [Fact]
        public async Task Should_Throw_Exception_When_Participant_Not_Found()
        {
            // Arrange
            int eventId = 1;
            int participantId = 1;

            _EventParticipantPersist
                .Setup(ep => ep.GetEventParticipantAsync(eventId, participantId))
                .ReturnsAsync((EventParticipant)null); 

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _eventParticipantServices.RemoveEventParticipant(eventId, participantId));

            _EventParticipantPersist.Verify(ep => ep.GetEventParticipantAsync(eventId, participantId), Times.Once);
            _EventParticipantPersist.Verify(ep => ep.RemoveParticipantFromEventAsync(eventId, participantId), Times.Never);
        }
    }

}
