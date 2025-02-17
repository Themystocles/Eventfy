using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventControllerTest.EventControllerDeleteTest
{
    public class EventControllerDeleteTest
    {
        private readonly Mock<IEventPersist> _eventPersistMock;
        private readonly EventService _eventServices;
        private readonly EventController _eventController;

       public EventControllerDeleteTest()
        {
            _eventPersistMock = new Mock<IEventPersist>();
            _eventServices = new EventService(_eventPersistMock.Object);
            _eventController = new EventController(_eventServices);
        }
        [Fact]
        public async Task ShouldReturn_Ok_When_Event_Deleted()
        {
            // Arrange
            var id = 1;
            var existingEvent = new Event
            {
                Id = id,
                Name = "Test",
                Description = "Test",
            };

            _eventPersistMock
                .Setup(ep => ep.GetEventByIdAsync(id)) 
                .ReturnsAsync(existingEvent);

            _eventPersistMock
                .Setup(ep => ep.DeleteEventAsync(id)) 
                .ReturnsAsync(existingEvent);

            // Act
            var result = await _eventController.DeleteEvent(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);

            _eventPersistMock.Verify(ep => ep.GetEventByIdAsync(id), Times.Once);
            _eventPersistMock.Verify(ep => ep.DeleteEventAsync(id), Times.Once);
        }

    }
}
