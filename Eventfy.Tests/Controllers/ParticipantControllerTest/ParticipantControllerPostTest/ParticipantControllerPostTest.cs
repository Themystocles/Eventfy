using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.ParticipantControllerTest.ParticipantControllerPostTest
{
    public class ParticipantControllerPostTest
    {
        private readonly Mock<IParticipantService> _participantservices;
        private readonly ParticipantController _participantcontroller;

        public ParticipantControllerPostTest()
        {
            _participantservices = new Mock<IParticipantService>();
            _participantcontroller = new ParticipantController(_participantservices.Object);
        }
        [Fact]
        public async Task Shouldreturn_OkObjectresultParticipantDto()
        {
            // Arrange
            var participantdto = new ParticipantDto
            {
                Id = 1,
                Name = "Test",
                Email = "Test@teste.com",
            };

            var participant = new Participant
            {
                Id= 1,  
                Name = participantdto.Name,
                Email = participantdto.Email,
            };
            _participantservices
              .Setup(x => x.CreateParticipantAsync(It.IsAny<ParticipantDto>()))
              .ReturnsAsync(participant);

            // Act
            var result = await _participantcontroller.AdicionarParticipantAsync(participantdto);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);

            var returnedParticipant = Assert.IsType<ParticipantDto>(okResult.Value);
            Assert.Equal(participantdto.Id, returnedParticipant.Id);
            Assert.Equal(participantdto.Name, returnedParticipant.Name);
            Assert.Equal(participantdto.Email, returnedParticipant.Email);
        }

    }
}