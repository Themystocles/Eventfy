using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Interface.Interface_Services;
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

        private readonly Mock<IEventServices> _eventServices;
        private readonly EventController _eventController;

        public EventControllerDeleteTest()
        {
            _eventServices = new Mock<IEventServices>();
            _eventController = new EventController(_eventServices.Object);
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

            _eventServices
                .Setup(ep => ep.GetEventById(id)) 
                .ReturnsAsync(existingEvent);

            _eventServices
                .Setup(ep => ep.DeleteEvent(id)) 
                .ReturnsAsync(true);

            // Act
            var result = await _eventController.DeleteEvent(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);

            
        }

    }
}
