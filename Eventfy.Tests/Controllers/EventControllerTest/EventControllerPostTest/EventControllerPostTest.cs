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

namespace Eventfy.Tests.Controllers.EventControllerTest.EventControllerPostTest
{
    public class EventControllerPostTest
    {

        private readonly Mock<IEventPersist> _eventPersistMock;
        private readonly EventService _eventService;
        private readonly EventController _eventController;
        public EventControllerPostTest() 
        {
            _eventPersistMock = new Mock<IEventPersist>();
            _eventService = new EventService(_eventPersistMock.Object);
            _eventController = new EventController(_eventService);
        }

        [Fact]
        public async Task Shouldreturn_OkObjectresultEventDto() 
        { 
            //Arrange

           var eventDto = new EventDto
           {
               Id = 1,
               Name = "test",
               Description = "Description test" 
           };
            var @event = new Event()
            {
                Id = eventDto.Id,
                Name = eventDto.Name,
                Description = eventDto.Description,
            };
            
            _eventPersistMock
                .Setup(ep => ep.CreateEvent(It.IsAny<Event>()))
                .ReturnsAsync(@event);
            //Act
            

            var result = await _eventController.PostEvent(eventDto);

            //Assert


            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
            var returnedEvent = Assert.IsType<EventDto>(okResult.Value);
            Assert.Equal(eventDto.Id, returnedEvent.Id);
            Assert.Equal(eventDto.Name, returnedEvent.Name);
            Assert.Equal(eventDto.Description, returnedEvent.Description);
            _eventPersistMock.Verify(ep => ep.CreateEvent(It.IsAny<Event>()), Times.Once);

        }
    }
}
