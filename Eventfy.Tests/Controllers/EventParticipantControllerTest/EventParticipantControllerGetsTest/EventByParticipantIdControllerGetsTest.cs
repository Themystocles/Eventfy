using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventParticipantControllerTest.EventParticipantControllerGetsTest
{
    public class EventByParticipantIdControllerGetsTest
    {
        private readonly Mock<IEventParticipantServices> _eventparticipantServicesMock;
        private readonly EventParticipantController _eventparticipantController;

        public EventByParticipantIdControllerGetsTest()
        {
            _eventparticipantServicesMock = new Mock<IEventParticipantServices>();
            _eventparticipantController = new EventParticipantController(_eventparticipantServicesMock.Object);
        }

        [Fact]
        public async Task ShouldReturn_EventByParticipantIdList()
        {
            // Arrange
            var participantId = 1;
            var eventparticipantMockList = new List<EventParticipant>()
            {
              new EventParticipant() { Id = 1, EventId = 1, ParticipantId = participantId },
              new EventParticipant() { Id = 2, EventId = 2, ParticipantId = participantId},
            };

            var @event = new List<Event>()
            {
                new Event() { Id = 1, Name="teste", Description = "description" },
                new Event() { Id = 2, Name="teste2", Description = "description2" },
            };
            _eventparticipantServicesMock
                .Setup(x => x.GetListEventByParticipantId(participantId))
                .ReturnsAsync(@event);

            // Act
            var result = await _eventparticipantController.GetEventByParticipantId(participantId);

            //assert
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEvets = Assert.IsType<List<Event>>(okresult.Value);

            Assert.NotEmpty(returnedEvets);
            Assert.Equal(2, returnedEvets.Count);
            Assert.Contains(returnedEvets, e => e.Id == 1);
            Assert.Contains(returnedEvets, e => e.Id == 2);
        }

    }
}
