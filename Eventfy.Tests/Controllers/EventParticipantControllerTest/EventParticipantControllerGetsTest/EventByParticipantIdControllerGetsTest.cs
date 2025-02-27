using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var ParticipantId = 1;
            var eventparticipantMockList = new List<EventParticipant>()
            {
              new EventParticipant() { Id = 1, EventId = 1, ParticipantId = ParticipantId },
          new EventParticipant() { Id = 2, EventId = 2, ParticipantId = ParticipantId},
            };

        }

    }
}
