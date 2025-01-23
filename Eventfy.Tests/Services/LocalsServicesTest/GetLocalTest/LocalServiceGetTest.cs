using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Persistence;
using Eventfy.Service;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.LocalsServicesTest.GetLocalTest
{
    public class LocalServiceGetTest
    {

        private readonly Mock<ILocalPersist> _MocklocalPersist;
        public LocalServiceGetTest()
        {
            _MocklocalPersist = new Mock<ILocalPersist>();
        }
        [Fact]
        public async Task ReturnAllLocalsAsync()
        {
            //Arrange
            var localFake = new List<Local>()
            {
                new Local { Id = 1, Capacidade = 100 , Endereco = "RJ"},
                new Local { Id = 2, Capacidade = 300 , Endereco = "SP"}
            };

            _MocklocalPersist
            .Setup(repo => repo.GetAllLocalAsync())
            .ReturnsAsync(localFake);

            var LocalService = new LocalService(_MocklocalPersist.Object);

            //act
            var local = await LocalService.GetAllLocalsAsync();

            //Assert
            Assert.NotNull(local);
            Assert.Equal(2, local.Count());
            Assert.Equal("RJ", local.First().Endereco);
        }
        [Fact]
        public async Task ShouldReturn_ThrowArgumentNullException()
        {
            //Arrange
            _MocklocalPersist.Setup(repo => repo.GetAllLocalAsync())
                .ReturnsAsync((IEnumerable<Local>)null);

            var localService = new LocalService(_MocklocalPersist.Object);
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
       () => localService.GetAllLocalsAsync()
   );

            Assert.Equal("locals", exception.ParamName);



        }
        [Fact]
        public async Task ShouldReturnALocalById()
        {
            //Arrange
           
            var localFake = new Local 
            { 
              Id = 1,
              Capacidade = 100000,
              Endereco = "Arena Castelão",
            };

            _MocklocalPersist
            .Setup(repo => repo.GetLocalByIdAsync(localFake.Id))
            .ReturnsAsync(localFake);

            var localService = new LocalService(_MocklocalPersist.Object);

            //act and Acert
            var result = await localService.GetLocalByIdAsync(localFake.Id);

            Assert.NotNull(result);
            Assert.Equal(localFake.Id, result.Id);
            Assert.Equal(localFake.Capacidade, result.Capacidade);
            Assert.Equal(localFake.Endereco, result.Endereco);


        }
        [Fact]
        public async Task IfLocalisnull_ShouldreturnThrowArgumentNullException()
        {
            
            _MocklocalPersist
                .Setup(repo => repo.GetLocalByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Local)null);

            var localService = new LocalService (_MocklocalPersist.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await localService.GetLocalByIdAsync(1);
            });
        }


    
    }
}
