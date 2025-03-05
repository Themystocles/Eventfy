using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventParticipantControllerTest.EventParticipantControllerPostTest
{
    public class EventParticipantControllerPostTest
    {
        private readonly Mock<IEventParticipantServices> _eventparticipantServicesMock;
        private readonly EventParticipantController _eventparticipantController;

        public EventParticipantControllerPostTest()
        {
            _eventparticipantServicesMock = new Mock<IEventParticipantServices>();
            _eventparticipantController = new EventParticipantController(_eventparticipantServicesMock.Object);
        }
        [Fact]

        public async Task ShouldReturn_OkEventparticipant()
        {
            var eventId = 1;
            var participantId = 1;
           
            var eventparticipantDto = new EventParticipantDto
            {
                EventId = eventId,
                ParticipantId = participantId,
            };
            var eventparticipant = new EventParticipant
            {
                EventId = eventparticipantDto.EventId,
                ParticipantId = eventparticipantDto.ParticipantId

            };

            _eventparticipantServicesMock
                .Setup(x => x.AddEventParticipant(eventparticipantDto))
                .ReturnsAsync(eventparticipant);

            var result = await _eventparticipantController.AddEventParticipant(eventId, participantId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEventParticipant = Assert.IsType<EventParticipantDto>(okResult.Value);

            Assert.Equal(eventparticipantDto.EventId, returnedEventParticipant.EventId);
            Assert.Equal(eventparticipantDto.ParticipantId, returnedEventParticipant.ParticipantId);






        }
    }
  
}
