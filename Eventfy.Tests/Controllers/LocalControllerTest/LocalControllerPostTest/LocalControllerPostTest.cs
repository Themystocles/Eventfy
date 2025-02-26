using Eventfy.Controllers;
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

namespace Eventfy.Tests.Controllers.LocalControllerTest.LocalControllerPostTest
{
    public class LocalControllerPostTest
    {
        private readonly Mock<ILocalServices> _localServices;
        private readonly LocalController _localcontroller;
        public LocalControllerPostTest()
        {
            _localServices = new Mock<ILocalServices>();
            _localcontroller = new LocalController(_localServices.Object);
        }
        [Fact]
        public async Task ShouldReturn_OkEventcreated()
        {
            //Arrange 
            var localdto = new LocalDto
            {
                Id = 1,
                Endereco = "Endereço",
                Capacidade = 200

            };

            var local = new Local
            {
                Id = localdto.Id,
                Endereco = localdto.Endereco,
                Capacidade = localdto.Capacidade

            };
           

            _localServices
                .Setup(x => x.CreateLocalAsync(It.IsAny<LocalDto>()))
                .ReturnsAsync(local);

            //act

            var result = await _localcontroller.AddLocalAsync(localdto);

            //Assert
            
            Assert.NotNull(result);
            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(okResult.Value);
            var returnedEvent = Assert.IsType<Local>(okResult.Value);
            Assert.Equal(localdto.Id, returnedEvent.Id);
            Assert.Equal(localdto.Endereco, returnedEvent.Endereco);
            Assert.Equal(localdto.Capacidade, returnedEvent.Capacidade);

        }

    }
}
