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
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.EventControllerTest.EventControllerPostTest
{
    public class EventControllerPostTest
    {
        private readonly Mock<IEventServices> _eventServices;
        private readonly EventController _eventController;
        public EventControllerPostTest() 
        {
            _eventServices = new Mock<IEventServices>();
            _eventController = new EventController(_eventServices.Object);
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

            _eventServices
                .Setup(ep => ep.CreateEvent(It.IsAny<EventDto>()))
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
            

        }
    }
}
