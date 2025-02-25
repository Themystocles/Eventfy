using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.ParticipantControllerTest.ParticipantControllerDeleteTest
{
    public class ParticipantControllerDeleteTest
    {

        private readonly Mock<IParticipantService> _participantservices;
        private readonly ParticipantController _participantcontroller;

        public ParticipantControllerDeleteTest()
        {
            _participantservices = new Mock<IParticipantService>();
            _participantcontroller = new ParticipantController(_participantservices.Object);
        }

        [Fact]
        public async Task Shouldreturn_OkParticipantDelete()
        {
            // Arrange
            var id = 1;
            var oarticipantExist = new Participant
            {
                Id = id,
                Name = "Test",
                Email = "Test",
            };

            _participantservices
                .Setup(ep => ep.GetParticipantByIdAsync(id))
                .ReturnsAsync(oarticipantExist);

            _participantservices
                .Setup(ep => ep.DeleteParticipantAsync(id))
                .ReturnsAsync(true);

            // Act
            var result = await _participantcontroller.DeleteParticipatAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);


        }
    }

    
}
