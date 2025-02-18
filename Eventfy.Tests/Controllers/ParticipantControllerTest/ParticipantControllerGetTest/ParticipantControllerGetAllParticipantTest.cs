using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.ParticipantControllerTest
{
    public class ParticipantControllerGetAllParticipantTest
    {
        private readonly Mock<IParticipantPersist> _participantPersistMock;
        private readonly ParticipantService _participantService;
        private readonly ParticipantController _participantController;

        public ParticipantControllerGetAllParticipantTest() 
        {
            _participantPersistMock = new Mock<IParticipantPersist>();
            _participantService = new ParticipantService(_participantPersistMock.Object);
            _participantController = new ParticipantController(_participantService);
        }

        [Fact]
        public async Task ShouldReturn_OkParticipantsList()
        {
            var participant = new List<Participant>
            {
                new Participant{Id= 1, Name = "Rochele", Email = "Rochele@gmail.com"},
                new Participant{Id= 2, Name = "Michael", Email = "Michael@gmail.com"}
            };

            _participantPersistMock
                .Setup(x => x.GetAllParticipantAsync())
                .ReturnsAsync(participant);
            var result = await _participantController.GetAllParticipant();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);

            var returnedParticipant = Assert.IsType<List<Participant>>(okResult.Value);
            Assert.Equal(2, returnedParticipant.Count);



        }



    }
}
