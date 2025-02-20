using Eventfy.Controllers;
using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.ParticipantControllerTest.ParticipantControllerGetTest
{
    public class ParticipantControllerGetParticipantByIdTest
    {
       // private readonly Mock<IParticipantPersist> _participantPersistMock;
        private readonly Mock<IParticipantService> _participantService;
        private readonly ParticipantController _participantController;
        public ParticipantControllerGetParticipantByIdTest()
        {
         //   _participantPersistMock = new Mock<IParticipantPersist>();
            _participantService = new Mock<IParticipantService>();
            _participantController = new ParticipantController(_participantService.Object);

        }
        [Fact]
        public async Task ShouldReturn_ParticipantById()
        {
            var id = 1;
            var participant = new Participant { 
                Id = id ,
                Name = "Test",
                Email =  "Teste@outlook.com"
                
            };

            _participantService
                .Setup(x => x.GetParticipantByIdAsync(id))
                .ReturnsAsync(participant);

            var result = await _participantController.GetParticipantById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Teste@outlook.com", result.Email);




        }
        [Fact]
        public async Task ShouldReturn_Null_When_Participant_Not_Found()
        {
            var id = 99; 

            _participantService
                .Setup(x => x.GetParticipantByIdAsync(id))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _participantController.GetParticipantById(id);

            Assert.Null(result); 
        }
    }



   
}
