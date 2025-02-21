using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventControllerTest.EventControllerPutTest
{
    public class EventControllerPutTest
    {
        private readonly Mock<IEventServices> _eventServices;
        private readonly EventController _eventController;
        public EventControllerPutTest() 
        {
            _eventServices = new Mock<IEventServices>();
            _eventController = new EventController(_eventServices.Object);

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

            var @event = new Event
            {
                Id = eventDto.Id,
                Name = eventDto.Name,
                Description = eventDto.Description

            };

            _eventServices
                .Setup(ep => ep.GetEventById(id))
                .ReturnsAsync(existingEvent);

            _eventServices
              .Setup(ep => ep.UpdateEvent(It.IsAny<EventDto>()))
                .ReturnsAsync(@event);

            // Act
            var result = await _eventController.PutEvent(id, eventDto);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
            var returnedEvent = Assert.IsType<Event>(okResult.Value);
            Assert.Equal(eventDto.Id, returnedEvent.Id);
            Assert.Equal(eventDto.Name, returnedEvent.Name);
            Assert.Equal(eventDto.Description, returnedEvent.Description);

            
        }
    }
}
