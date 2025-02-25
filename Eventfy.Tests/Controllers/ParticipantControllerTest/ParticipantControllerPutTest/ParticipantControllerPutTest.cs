using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.ParticipantControllerTest.ParticipantControllerPutTest
{
    public class ParticipantControllerPutTest
    {
        private readonly Mock<IParticipantService> _participantservices;
        private readonly ParticipantController _participantcontroller;

        public ParticipantControllerPutTest()
        {
            _participantservices = new Mock<IParticipantService>();
            _participantcontroller = new ParticipantController(_participantservices.Object);
        }
        [Fact]
        public async Task shouldReturn_ParticipantPut()
        {
            //Arrange
            var id = 1;
            var existingParticipant = new Participant
            {
                Id = id,
                Name = "Participant existents",
                Email = "participant@teste.com",

            };
            var participantDto = new ParticipantDto
            {
                Id = id,
                Name = "Participant edit",
                Email = "ParticipantEdit@teste.com.br"

            };
            var participant = new Participant
            {
                Id = id,
                Name = participantDto.Name,
                Email = participantDto.Email,

            };
            _participantservices
                .Setup(x => x.GetParticipantByIdAsync(id))
                .ReturnsAsync(participant);

            _participantservices
                .Setup(x => x.UpdateParticipantAsync(participantDto))
                .ReturnsAsync(participant);
            //Act
            var result = await _participantcontroller.UpdateParticipantAsync(id, participantDto);

            //Assert

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
            var returnedParticipant = Assert.IsType<ParticipantDto>(okResult.Value);
            Assert.Equal(participantDto.Id, returnedParticipant.Id);
            Assert.Equal(participantDto.Name, returnedParticipant.Name);
            Assert.Equal(participantDto.Email, returnedParticipant.Email);

            _participantservices.Verify(x => x.GetParticipantByIdAsync(id), Times.Once);



        }
       
    }
}
