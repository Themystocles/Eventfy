using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.ParticipantServicesTest.GetParticipantTest
{
    public class ParticipantServiceGetTest
    {
        private readonly Mock<IParticipantPersist> _participantPersist;

        public ParticipantServiceGetTest()
        {
               _participantPersist = new Mock<IParticipantPersist>();
        }
        [Fact]
        public async Task ShouldReturnAllParticipants()
        {
            //Arrange
            var lisparticipant = new List<Participant>()
            {
                new Participant() { Id = 1, Email = "Thiago@outlook.com", Name = "Thiago"},
                new Participant() { Id = 2, Email = "Rodrigues@outlook.com", Name = "Rodrigues"}

            };
            _participantPersist
                .Setup(repo => repo.GetAllParticipantAsync())
                .ReturnsAsync(lisparticipant);

            var service = new ParticipantService(_participantPersist.Object);

            //Act
            var result = await service.GetAllParticipants();

            //assert

            Assert.Equal(lisparticipant, result);
            Assert.NotNull(lisparticipant);
            Assert.Equal(2, lisparticipant.Count());
            Assert.Equal("Thiago@outlook.com", result.First().Email);
        }
        [Fact]
        public async Task ShouldReturnAParticipantById()
        {
            //Arrange
            var mockParticipant = new Participant()
            {
                Id = 1,
                Name = "Themystocles",
                Email = "Themystoclers@outlook.com"
            };

            _participantPersist
                .Setup(repo => repo.GetParticipantByIdAsync(mockParticipant.Id))
                .ReturnsAsync(mockParticipant);

            var service = new ParticipantService(_participantPersist.Object);

            //act
            var result = await service.GetParticipantByIdAsync(mockParticipant.Id);

            //assert
            Assert.NotNull(result);
            Assert.Equal(mockParticipant.Id, result.Id);
            Assert.Equal(mockParticipant.Name, result.Name);
            Assert.Equal(mockParticipant.Email, result.Email);


        }
        [Fact]
        public async Task ShouldReturn_KeyNotFoundException()
        {
          //Arrange
            _participantPersist
                .Setup(repo => repo.GetParticipantByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Participant)null);

            var service = new ParticipantService(_participantPersist.Object);

            //act && Assert

            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await service.GetParticipantByIdAsync(1);
            });
        }
    }
    
}
