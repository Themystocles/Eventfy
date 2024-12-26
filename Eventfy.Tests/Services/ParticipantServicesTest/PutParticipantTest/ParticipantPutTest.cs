using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Eventfy.Tests.Services.ParticipantServicesTest.PutParticipantTest
{
    public class ParticipantPutTest
    {
        private readonly Mock<IParticipantPersist> _participantPersist;

        public ParticipantPutTest()
        {
            _participantPersist = new Mock<IParticipantPersist>();
        }
        [Fact]
        public async Task ShouldUpdateParticipant()
        {
            var Participantexistent = new Participant()
            {
                Id = 1,
                Name = "Mauricio",
                Email = "Mauricio@outlook.com",
            };
            var ParticipantDto = new ParticipantDto()
            {
                Id = 1,
                Name = "Ronaldo",
                Email = "Ronaldo@outlook.com", 
            };

            var updatedparticipant = new Participant()
            {
                Id = 1,
                Name = ParticipantDto.Name,
                Email = ParticipantDto.Email,
            };

            _participantPersist
                .Setup(repo => repo.GetParticipantByIdAsync(ParticipantDto.Id))
                .ReturnsAsync(Participantexistent);

            _participantPersist
                .Setup(repo => repo.UpdateParticipantAsync(It.IsAny<Participant>()))
                .ReturnsAsync(updatedparticipant);

            var service = new ParticipantService(_participantPersist.Object);

            var result = await service.UpdateParticipantAsync(ParticipantDto);

            Assert.NotNull(result);
            Assert.Equal(updatedparticipant.Name, result.Name);
            Assert.Equal(updatedparticipant.Email, result.Email);
        }
        [Fact]
        public async Task showldreturn_ArgumentNullException()
        {
            _participantPersist
                .Setup(repo => repo.UpdateParticipantAsync(It.IsAny<Participant>()))
                .ReturnsAsync((Participant) null);

            var service = new ParticipantService(_participantPersist.Object);


            var result = Assert.ThrowsAsync<ArgumentNullException>(
                () => service.UpdateParticipantAsync(null));
        }




    }
}
