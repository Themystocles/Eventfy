using Eventfy.Interface;
using Eventfy.Models.DTOs;
using Eventfy.Models;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.EventsServicesTest.PostEventTest
{
    public class PostEventServiceTest
    {
        [Fact]
        public async Task CreateEvent_ShouldReturn_EventDTOCreate()
        {
            var mockEventPersist = new Mock<IEventPersist>();
            // Arrange
            var eventDto = new EventDto
            {
                Id = 0,
                Name = "Btc o futuro",
                Description = "Entenda tudo sobre Biticoins",
                DateEvent = DateTime.Now,
            };
            var createdEvent = new Event
            {
                Id = 1,
                Name = eventDto.Name,
                Description = eventDto.Description,
                DateEvent = eventDto.DateEvent,

            };
            mockEventPersist.Setup(repo => repo.CreateEvent(It.IsAny<Event>()))
                          .ReturnsAsync(createdEvent);

            var service = new EventService(mockEventPersist.Object);

            //act
            var result = await service.CreateEvent(eventDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdEvent.Name, result.Name);
            Assert.Equal(createdEvent.Description, result.Description);
            Assert.Equal(createdEvent.DateEvent, result.DateEvent);

            // Verifica se o método CreateEvent foi chamado com os parâmetros corretos
            mockEventPersist.Verify(repo => repo.CreateEvent(It.Is<Event>(e =>
                e.Name == eventDto.Name &&
                e.Description == eventDto.Description &&
                e.DateEvent == eventDto.DateEvent
            )), Times.Once);
        }
    }
}
