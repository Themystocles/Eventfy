using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Eventfy.Tests.Controllers.EventControllerGetsTest
{
    public class GetEventByIdControllerTest
    {
     
        private readonly Mock<IEventServices> _eventService;
        private readonly EventController _eventController;

        public GetEventByIdControllerTest()
        {
           
            _eventService = new Mock<IEventServices>();
            _eventController = new EventController(_eventService.Object);
        }

        [Fact]
        public async Task ShouldReturn_EventById()
        {
            // Arrange
            var eventMock = new Event()
            {
                Id = 1,
                Name = "Maria",
                Description = "Técnica em sistemas",
            };

            _eventService.Setup(ep => ep.GetEventById(eventMock.Id))
                .ReturnsAsync(eventMock);

            var result = await _eventController.GetEventById(1);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);


            var returnedEvent = Assert.IsType<Event>(okResult.Value);

            Assert.NotNull(returnedEvent);
            Assert.Equal(1, returnedEvent.Id);
            Assert.Equal("Maria", returnedEvent.Name);
            Assert.Equal("Técnica em sistemas", returnedEvent.Description);
        }
    }
}
