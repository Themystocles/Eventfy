using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventParticipantControllerTest.EventParticipantControllerDeleteTest
{
    public class EventParticipanteControllerDeleteTest
    {
        private readonly Mock<IEventParticipantServices> _eventparticipantServicesMock;
        private readonly EventParticipantController _eventparticipantController;

        public EventParticipanteControllerDeleteTest()
        {
            _eventparticipantServicesMock = new Mock<IEventParticipantServices>();
            _eventparticipantController = new EventParticipantController(_eventparticipantServicesMock.Object);
        }
        [Fact]
        public async Task SholdReturn_EventParticipantDeleted()
        {

            // Arrange
            var eventId = 1;
            var participantId = 2;

            _eventparticipantServicesMock
                .Setup(x => x.RemoveEventParticipant(eventId, participantId))
                .Returns(Task.CompletedTask); 

            // Act
            var result = await _eventparticipantController.DeleteEventParticipant(eventId, participantId);

            // Assert
            Assert.IsType<NoContentResult>(result);


        }
    }
}
