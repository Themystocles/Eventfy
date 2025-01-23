using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.EventsServicesTest.DeletEventTest
{
    public class DeleteEventServiceTest
    {
        private readonly Mock<IEventPersist> _mockEventPersist;
        private readonly EventService _service;

        public DeleteEventServiceTest()
        {
            // Inicializando o mock e o serviço no construtor
            _mockEventPersist = new Mock<IEventPersist>();
            _service = new EventService(_mockEventPersist.Object);
        }

        [Fact]
        public async Task DeleteEvent_ShouldReturnTrue_WhenEventIsDeleted()
        {
            // Arrange
            var eventId = 1; // ID do evento a ser deletado
            var existingEvent = new Event
            {
                Id = eventId,
                Name = "Btc o futuro",
                Description = "Entenda tudo sobre Biticoins",
                DateEvent = DateTime.Now,
            };

            // Configura o mock para retornar o evento existente ao buscar por ID
            _mockEventPersist.Setup(repo => repo.GetEventByIdAsync(eventId))
                             .ReturnsAsync(existingEvent);

            // Configura o mock para simular a remoção do evento
            _mockEventPersist.Setup(repo => repo.DeleteEventAsync(eventId))
                  .ReturnsAsync(existingEvent);  // Retorna o evento deletado (pode ser o mesmo ou um novo)




            // Act
            var result = await _service.DeleteEvent(eventId);

            // Assert
            Assert.True(result); // Verifica se o retorno é true, indicando que o evento foi deletado

            // Verifica se o método DeleteEventAsync foi chamado com o ID correto
            _mockEventPersist.Verify(repo => repo.DeleteEventAsync(eventId), Times.Once);
        }

        
    }
}
