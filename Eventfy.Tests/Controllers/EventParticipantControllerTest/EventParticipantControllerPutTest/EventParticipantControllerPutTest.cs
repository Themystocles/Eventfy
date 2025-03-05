using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Persistence;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventParticipantControllerTest.EventParticipantControllerPutTest
{
    public class EventParticipantControllerPutTest
    {
        private readonly Mock<IEventParticipantServices> _eventparticipantServicesMock;
        private readonly EventParticipantController _eventparticipantController;
        public EventParticipantControllerPutTest() 
        {
            _eventparticipantServicesMock = new Mock<IEventParticipantServices>();
            _eventparticipantController = new EventParticipantController(_eventparticipantServicesMock.Object);
        }
        [Fact]
        public async Task ShouldReturn_OkEventParticipant()
        {
            var EventParticipantId = 1;

            var eventParticipantDto = new EventParticipantDto
            {
                Id = EventParticipantId,
                EventId = 1,
                ParticipantId = 1

            };
            var eventParticipant = new EventParticipant
            {
                Id = EventParticipantId,
                EventId = eventParticipantDto.EventId,
                ParticipantId = eventParticipantDto.ParticipantId

            };

            _eventparticipantServicesMock
                .Setup(x => x.UpdateEventParticipantAsync(It.IsAny<EventParticipantDto>()))
                .ReturnsAsync(eventParticipant);

            var result = await _eventparticipantController.UpdateEventParticipant(EventParticipantId, eventParticipantDto);

            var okObjctResult = Assert.IsType<OkObjectResult>(result.Result);
            var ReturnEventParticipant = Assert.IsType<EventParticipant>(okObjctResult.Value);



            Assert.Equal(eventParticipantDto.EventId, ReturnEventParticipant.EventId);
            Assert.Equal(eventParticipantDto.ParticipantId, ReturnEventParticipant.ParticipantId);
        }
     
    } 
}
