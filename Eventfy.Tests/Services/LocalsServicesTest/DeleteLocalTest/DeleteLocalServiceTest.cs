using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Service;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.LocalsServicesTest.DeleteLocalTest
{
    public class DeleteLocalServiceTest
    {
        private readonly Mock<ILocalPersist> _mockLocalPersist;
        public DeleteLocalServiceTest()
        {
            _mockLocalPersist = new Mock<ILocalPersist>();
        }
        [Fact]
        public async Task DeleteLocal_ShouldReturnTrue_WhenLocalIsDeleted()
        {
            // Arrange
            var localExist = new Local()
            {
                Id = 1,
                Endereco = "Recife",
                Capacidade = 200
            };

            _mockLocalPersist
                .Setup(repo => repo.GetLocalByIdAsync(localExist.Id))
                .ReturnsAsync(localExist);

            _mockLocalPersist
                .Setup(repo => repo.DeleteLocalAsync(localExist))
                .ReturnsAsync(localExist);

            var service = new LocalService(_mockLocalPersist.Object);

            // Act
            var result = await service.DeleteLocal(localExist.Id);

            // Assert
            Assert.True(result);
            _mockLocalPersist.Verify(repo => repo.GetLocalByIdAsync(localExist.Id), Times.Once);
            _mockLocalPersist.Verify(repo => repo.DeleteLocalAsync(localExist), Times.Once);
        }
    }
}
