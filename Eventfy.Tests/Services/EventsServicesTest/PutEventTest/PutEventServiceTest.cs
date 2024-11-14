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
using System.ComponentModel;

namespace Eventfy.Tests.Services.EventsServicesTest.PutEventTest
{
    public class PutEventServiceTest
    {
        [Fact]
        public async Task UpdateEvent_ShouldReturn_EventDTOUpdate()
        {
            var mockEventPersist = new Mock<IEventPersist>();
            // Arrange
            var eventDto = new EventDto
            {
                Id = 1,
                Name = "Btc o futuro",
                Description = "Entenda tudo sobre Biticoins",
                DateEvent = DateTime.Now,
            };
            var existingEvent = new Event
            {
                Id = 1,
                Name = "Evento Antigo",
                Description = "Descrição Antiga",
                DateEvent = DateTime.Now.AddDays(-1),

            };
            var updateEvent = new Event
            {
                Id = 1,
                Name = eventDto.Name,
                Description = eventDto.Description,
                DateEvent = eventDto.DateEvent,

            };
            //Pega o Evento Por ID
            mockEventPersist.Setup(repo => repo.GetEventByIdAsync(eventDto.Id))
            .ReturnsAsync(existingEvent);

            mockEventPersist.Setup(repo => repo.UpdateEventAsync(It.IsAny<Event>()))
                .ReturnsAsync(updateEvent);

            var service = new EventService(mockEventPersist.Object);

            //act
            var result = await service.UpdateEvent(eventDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateEvent.Name, result.Name);
            Assert.Equal(updateEvent.Description, result.Description);
            Assert.Equal(updateEvent.DateEvent, result.DateEvent);

            // Verifica se o método CreateEvent foi chamado com os parâmetros corretos
            mockEventPersist.Verify(repo => repo.UpdateEventAsync(It.Is<Event>(e =>
         e.Id == eventDto.Id &&
         e.Name == eventDto.Name &&
         e.Description == eventDto.Description &&
         e.DateEvent == eventDto.DateEvent
     )), Times.Once);
        }

        [Fact]
        public async Task UpdateEvent_ShouldThrow_ArgumentNullException_WhenEventNoExist()
        {
            //Asset
            var mockEventPersist = new Mock<IEventPersist>();
            var service = new EventService(mockEventPersist.Object);

            //act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
         () => service.UpdateEvent(null)
        );

        Assert.Equal("O evento não pode ser nulo. (Parameter 'Updatevent')", exception.Message);

            mockEventPersist.Verify(repo => repo.GetEventByIdAsync(It.IsAny<int>()), Times.Never);
            mockEventPersist.Verify(repo => repo.UpdateEventAsync(It.IsAny<Event>()), Times.Never);


            
        }
        [Fact]
        public async Task UpdateEvent_ShouldThrow_KeyNotFoundException_WhenEventDoesNotExist()
        {
            // Arrange
            var mockEventPersist = new Mock<IEventPersist>();
            var service = new EventService(mockEventPersist.Object);

            var eventDto = new EventDto
            {
                Id = 1, // ID que não existe no repositório
                Name = "Btc o futuro",
                Description = "Entenda tudo sobre Biticoins",
                DateEvent = DateTime.Now,
            };

            // Configura o mock para retornar null quando procurar pelo evento com o ID especificado
            mockEventPersist.Setup(repo => repo.GetEventByIdAsync(eventDto.Id))
                            .ReturnsAsync((Event)null); // Simula que o evento não foi encontrado

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateEvent(eventDto));

            // Valida a mensagem da exceção (opcional)
            Assert.Equal($"Evento com ID {eventDto.Id} não encontrado.", exception.Message);

            // Verifica que o método UpdateEventAsync não foi chamado
            mockEventPersist.Verify(repo => repo.UpdateEventAsync(It.IsAny<Event>()), Times.Never);
        }




    }
}
