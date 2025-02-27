using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventParticipantControllerTest.EventParticipantControllerGetsTest
{
    public class ParticipantByEventIdControllerGetsTest
    {
        private readonly Mock<IEventParticipantServices> _eventparticipantServicesMock;
        private readonly EventParticipantController _eventparticipantController;
        

        public ParticipantByEventIdControllerGetsTest()
        {
           _eventparticipantServicesMock = new Mock<IEventParticipantServices>();
           _eventparticipantController = new EventParticipantController(_eventparticipantServicesMock.Object);
        }
        [Fact]
        public async Task ShouldReturn_ParticipantByEventIdList()
        {
            // Arrange
            var eventId = 1;
            var eventparticipantMockList = new List<EventParticipant>()
    {
        new EventParticipant() { Id = 1, EventId = eventId, ParticipantId = 1 },
        new EventParticipant() { Id = 2, EventId = eventId, ParticipantId = 2 },
    };

            var participant = new List<Participant>()
    {
        new Participant() { Id = 1, Name = "Teste", Email = "Teste@teste.com" },
        new Participant() { Id = 2, Name = "Teste2", Email = "Teste2@teste.com" }
    };

            _eventparticipantServicesMock
                .Setup(x => x.GetListParticipantByEventId(eventId))
                .ReturnsAsync(participant);

            // Act
            var result = await _eventparticipantController.GetParticipantByEventId(eventId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result); 
            var returnedParticipants = Assert.IsType<List<Participant>>(okResult.Value); 

            Assert.NotNull(returnedParticipants);
            Assert.Equal(2, returnedParticipants.Count);
            Assert.Contains(returnedParticipants, p => p.Id == 1);
            Assert.Contains(returnedParticipants, p => p.Id == 2);
        }

    }
}
