using Eventfy.Models;
using Eventfy.Controllers;
using Eventfy.Service;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eventfy.Interface;
using Xunit;
using Eventfy.Interface.Interface_Services;

namespace Eventfy.Tests.EventControllerGetsTest
{
    public class EventControllerGetAllEventTest
    {
      
        private readonly Mock<IEventServices> _eventServices;
        private readonly EventController _eventController;

        public EventControllerGetAllEventTest()
        {
       
            _eventServices = new Mock<IEventServices>();
            _eventController = new EventController(_eventServices.Object);
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

            _eventServices
                .Setup(ev => ev.GetEvents())
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
