using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Eventfy.Interface;
using Eventfy.Service;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Persistence;

namespace Eventfy.Tests.Services.EventParticipantServicesTest
{
    public class PostEventsParticipantTest
    {
        private readonly Mock<IEventParticipantPersist> _EventParticipantPersist;
        private readonly Mock<IEventPersist> _eventPersistMock;
        private readonly Mock<IParticipantPersist> _participantPersistMock;
        private readonly EventParticipantServices _eventParticipantServices;

        public PostEventsParticipantTest()
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

        public async Task PostEventParticipant_ReturnNewEventParticipant()
        {
            //Arrange
            int eventId = 1;    
            int participantId = 1;

            var evento = new Event { Id = eventId, Name = "Evento Teste" };
            _eventPersistMock
             .Setup(ep => ep.GetEventByIdAsync(eventId))
             .ReturnsAsync(evento);

            var participant = new Participant { Id = participantId, Name = "NomeTeste" };
            _participantPersistMock
             .Setup(ep => ep.GetParticipantByIdAsync(participantId))
             .ReturnsAsync(participant);

            var eventParticipant = new EventParticipant()
            {
                Id = 1,
                EventId = eventId,
                ParticipantId = participantId

            };

            var eventParticipantDto = new EventParticipantDto()
            {
                Id = eventParticipant.Id,
                ParticipantId = eventParticipant.ParticipantId,
                EventId = eventParticipant.EventId
            };

            _EventParticipantPersist
                .Setup(ep => ep.AddParticipantToEventAsync(eventId, participantId))
                . ReturnsAsync(eventParticipant);

            //act

            var result = await _eventParticipantServices.AddEventParticipant(eventParticipantDto);

            Assert.Equal(eventId, result.EventId);
            Assert.Equal(participantId, result.ParticipantId);
            Assert.Equal(eventParticipant.Id, result.Id);




        }
        [Fact]
        public async Task EventNull_Shouldreturn_KeyNotFoundException()
        {
            //arrange
            var eventParticipantdto = new EventParticipantDto()
            {
                EventId = 1,
                ParticipantId = 1
            };
            _eventPersistMock
                .Setup(ep => ep.GetEventByIdAsync(eventParticipantdto.EventId))
                .ReturnsAsync((Event)null);



            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
             _eventParticipantServices.AddEventParticipant(eventParticipantdto));

            Assert.Equal("O Evento não foi encontrado.", exception.Message); ;


        }
        [Fact]
        public async Task ParticipantNull_Shouldreturn_KeyNotFoundException()
        {
            //arrange
            var eventParticipantdto = new EventParticipantDto()
            {
                EventId = 1,
                ParticipantId = 1
            };
            _eventPersistMock
             .Setup(ep => ep.GetEventByIdAsync(eventParticipantdto.EventId))
             .ReturnsAsync(new Event { Id = 1, Name = "Evento Teste" });
            _participantPersistMock
                .Setup(ep => ep.GetParticipantByIdAsync(eventParticipantdto.ParticipantId))
                .ReturnsAsync((Participant)null);



            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
             _eventParticipantServices.AddEventParticipant(eventParticipantdto));

            Assert.Equal("O Participante não foi encontrado.", exception.Message); 


        }
    }
}
