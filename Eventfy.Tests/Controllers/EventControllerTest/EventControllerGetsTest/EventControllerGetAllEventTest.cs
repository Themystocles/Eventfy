using Eventfy.Models;
using Eventfy.Controllers;
using Eventfy.Service;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eventfy.Interface;
using Xunit;

namespace Eventfy.Tests.EventControllerGetsTest
{
    public class EventControllerGetAllEventTest
    {
        private readonly Mock<IEventPersist> _eventPersistMock;
        private readonly EventService _eventServices;
        private readonly EventController _eventController;

        public EventControllerGetAllEventTest()
        {
            _eventPersistMock = new Mock<IEventPersist>();
            _eventServices = new EventService(_eventPersistMock.Object);
            _eventController = new EventController(_eventServices);
        }

        [Fact]
        public async Task GetAllEventsAsync_ShouldReturnOk_WithListOfEvents()
        {
            // Arrange
            var mockEvents = new List<Event>
            {
                new Event { Id = 1, Name = "Event um" },
                new Event { Id = 2, Name = "event dois" }
            };

            _eventPersistMock
                .Setup(ev => ev.GetAllEventAsync())
                .ReturnsAsync(mockEvents);
            // Act
            var result = await _eventController.GetAllEventsAssync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value); 

            var returnedEvents = Assert.IsType<List<Event>>(okResult.Value);
            Assert.Equal(2, returnedEvents.Count);


        }
    }
}
