using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.LocalsServicesTest.PutLocalTest
{
    public class PutLocalServiceTeste
    {
        private readonly Mock<ILocalPersist> _mockLocalService;
        public PutLocalServiceTeste()
        {
            _mockLocalService = new Mock<ILocalPersist>();
        }
        [Fact]
        public async Task ShouldVerify_LocalUpdate() 
        {
            // Arrange
            var existentLocal = new Local()
            {
                Id = 1,
                Capacidade = 200,
                Endereco =  "Rua Natal",
                
            };
            var localDto = new LocalDto()
            {
                Id = 1,
                Capacidade = 500,
                Endereco = "Rua Pascoa"
            };

            var Updatelocal = new Local()
            {
                Id = localDto.Id,
                Capacidade = localDto.Capacidade,
                Endereco = localDto.Endereco,
            };

            _mockLocalService
                .Setup(Repo => Repo.GetLocalByIdAsync(localDto.Id))
                .ReturnsAsync(existentLocal);

            _mockLocalService
                .Setup(Repo => Repo.UpdateLocalAsync(It.IsAny<Local>()))
                .ReturnsAsync(Updatelocal);

            
            var service = new LocalService(_mockLocalService.Object);
            // act 
            var result = await service.UpdateLocalAsync(localDto);

            //asset

            Assert.NotNull(result);
            Assert.Equal(Updatelocal.Capacidade, result.Capacidade);
            Assert.Equal(Updatelocal.Endereco, result.Endereco);
            




        }
        [Fact]
       public async Task Shouldreturn_ArgumentNullException()
        {
            var service = new LocalService(_mockLocalService.Object);

            var  exception = await Assert.ThrowsAsync<ArgumentNullException>(
                ()=> service.UpdateLocalAsync(null));

           Assert.Equal("O parâmetro 'localdto' não pode ser nulo. (Parameter 'localdto')", exception.Message);


        }
        [Fact]
        public async Task Shouldreturn_LocalExistArgumentNullException()
        {
            // Arrange
            var localDto = new LocalDto { Id = 1 }; 
            _mockLocalService
                .Setup(p => p.GetLocalByIdAsync(localDto.Id))
                .ReturnsAsync((Local)null); 

            var service = new LocalService(_mockLocalService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => service.UpdateLocalAsync(localDto));

            Assert.Equal("O Local não foi encontrado. (Parameter 'localExistente')", exception.Message);

        }
    }
}
