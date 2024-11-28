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

namespace Eventfy.Tests.Services.ParticipantServicesTest.GetParticipantTest
{
    
    public class ParticipantPostTest
    {
        private readonly Mock<IParticipantPersist> _participantPersist;
        public ParticipantPostTest()
        {
            _participantPersist = new Mock<IParticipantPersist>();
        }
        [Fact]
        public async Task ShouldReturnParticipanteCreate()
        {
            var mockParticipantDto = new ParticipantDto()
            {
                Id = 1,
                Name = "Crtistiano Ronaldo",
                Email = "Ronaldo@TheBest.com"
            };
            var participant = new Participant()
            {
                Id = mockParticipantDto.Id,
                Name = mockParticipantDto.Name,
                Email = mockParticipantDto.Email

            };
            _participantPersist
                .Setup(repo => repo.CreateParticipantAsync(participant))
                .ReturnsAsync(participant);

            var service = new ParticipantService(_participantPersist.Object);

            //act

            var result = await service.CreateParticipantAsync(mockParticipantDto);

            //assert

            Assert.NotNull(result);
            Assert.Equal(mockParticipantDto.Id, result.Id);
            Assert.Equal(mockParticipantDto.Name, result.Name);
            Assert.Equal(mockParticipantDto.Email, result.Email);



        }
        [Fact]
        public async Task shouldReturn_ArgumentNullException()
        {
            _participantPersist
                .Setup(repo => repo.CreateParticipantAsync(It.IsAny<Participant>()))
                .ReturnsAsync((Participant) null);

            var service = new ParticipantService(_participantPersist.Object);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
           service.CreateParticipantAsync(null));
           


        }
    }
}
