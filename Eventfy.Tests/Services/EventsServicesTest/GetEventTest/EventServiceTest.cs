using Eventfy.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Eventfy.Data;
using Eventfy.Persistence;
using Eventfy.Interface;
using Eventfy.Service;
using Microsoft.Extensions.Logging;
using Eventfy.Models.DTOs;

namespace Eventfy.Tests.Services.EventsServicesTest.GetEventTest
{
    public class EventServiceTest
    {
        private readonly Mock<IEventPersist> _mockEventPersist;
        public EventServiceTest()
        {
            _mockEventPersist = new Mock<IEventPersist>();
        }   

        [Fact]
        public async Task GetAllEventAsync_ShouldReturnAllEvents()
        {
            //Arrange
           
            // Mock Data
            var eventFake = new List<Event>
        {
            new Event { Id = 1, Name = "Evento 1", Description = "Description 1"},
            new Event { Id = 2, Name = "Evento 2", Description = "Description 2"}
        };
            _mockEventPersist
           .Setup(repo => repo.GetAllEventAsync())
           .ReturnsAsync(eventFake);

            var eventService = new EventService(_mockEventPersist.Object);

            // Act
            var events = await eventService.GetEvents();

            // Assert
            Assert.NotNull(events);
            Assert.Equal(2, events.Count());
            Assert.Equal("Evento 1", events.First().Name);
        }
        [Fact]
        public async Task GetEventById_ShouldReturnAEvent()
        {
            //Arrange
      

            // Mock Data
            var id = 1;
            var Name = "Fabio";
            var Description = "Uma breve descrição";
            var @eventFake = new Event { Id = id, Name = Name, Description = Description };


            _mockEventPersist
           .Setup(repo => repo.GetEventByIdAsync(id))
           .ReturnsAsync(@eventFake);

            var eventService = new EventService(_mockEventPersist.Object);

            // Act
            var @event = await eventService.GetEventById(id);

            // Assert
            Assert.NotNull(@event);
            Assert.Equal("Fabio", @event.Name);
            Assert.Equal("Uma breve descrição", @event.Description);
        }
        [Fact]
        public async Task GetEventById_ShouldReturnNull_WhenEventDoesNotExist()
        {
            // Arrange

            // Mock Data
            var idExistente = 1;
            var idInexistente = 2;
            var @eventFake = new Event { Id = idExistente, Name = "Fabio", Description = "Uma breve descrição" };


            _mockEventPersist
                .Setup(repo => repo.GetEventByIdAsync(idExistente))
                .ReturnsAsync(@eventFake);

            var eventService = new EventService(_mockEventPersist.Object);

            // Act
            var @event = await eventService.GetEventById(idInexistente);

            // Assert
            Assert.Null(@event);
        }
        
    }
}