using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.ParticipantServicesTest.DeleteParticipanttTest
{
    public class DeleteParticipantServiceTest
    {
        private readonly Mock<IParticipantPersist> _participantPersist;

        public DeleteParticipantServiceTest()
        {
            _participantPersist = new Mock<IParticipantPersist>();
        }

        [Fact]
        public async Task DeleteParticipant_shouldReturnTrue_WhenParticipantDeleted()
        {
            //Arrage
            var participantExist = new Participant
            {
                Id = 1,
                Name = "Samuel",
                Email = "Samuel@outlook.com"
            };
            _participantPersist
                .Setup(repo => repo.GetParticipantByIdAsync(participantExist.Id))
                .ReturnsAsync(participantExist);

            _participantPersist
                .Setup(repo => repo.DeleteParticipantAsync(participantExist))
                .ReturnsAsync(participantExist);

            var service = new ParticipantService(_participantPersist.Object);
            
            //act
            var result = await service.DeleteParticipantAsync(participantExist.Id);

            //Assert

            Assert.True(result);


        }
    }
}
