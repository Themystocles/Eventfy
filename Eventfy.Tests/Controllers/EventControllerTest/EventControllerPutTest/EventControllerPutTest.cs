using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventControllerTest.EventControllerPutTest
{
    public class EventControllerPutTest
    {
        private readonly Mock<IEventPersist> _eventPersistMock;
        private readonly EventService _eventService;
        private readonly EventController _eventController;
        public EventControllerPutTest() 
        {
            _eventPersistMock = new Mock<IEventPersist>();
            _eventService = new EventService(_eventPersistMock.Object);
            _eventController = new EventController(_eventService); ;

    }
        [Fact]
        public async Task ShouldReturn_EventPut()
        {
            // Arrange
            var id = 1;
            var existingEvent = new Event
            {
                Id = id,
                Name = "Existing Event",
                Description = "Existing Description"
            };

            var eventDto = new EventDto
            {
                Id = id,
                Name = "Updated Event",
                Description = "Updated Description"
            };

            _eventPersistMock
                .Setup(ep => ep.GetEventByIdAsync(id))
                .ReturnsAsync(existingEvent); 

            _eventPersistMock
                .Setup(ep => ep.UpdateEventAsync(It.IsAny<Event>()))
                .ReturnsAsync((Event e) => e); 

            // Act
            var result = await _eventController.PutEvent(id, eventDto);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
            var returnedEvent = Assert.IsType<EventDto>(okResult.Value);
            Assert.Equal(eventDto.Id, returnedEvent.Id);
            Assert.Equal(eventDto.Name, returnedEvent.Name);
            Assert.Equal(eventDto.Description, returnedEvent.Description);

            _eventPersistMock.Verify(ep => ep.GetEventByIdAsync(id), Times.Once);
            _eventPersistMock.Verify(ep => ep.UpdateEventAsync(It.IsAny<Event>()), Times.Once);
        }
    }
}
