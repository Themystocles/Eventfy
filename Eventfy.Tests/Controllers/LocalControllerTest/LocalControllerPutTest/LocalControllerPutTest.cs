using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Eventfy.Tests.Controllers.LocalControllerTest.LocalControllerPutTest
{
    public class LocalControllerPutTest
    {
        private readonly Mock<ILocalServices> _localServices;
        private readonly LocalController _localcontroller;
        public LocalControllerPutTest() 
        {
            _localServices = new Mock<ILocalServices>();
            _localcontroller = new LocalController(_localServices.Object);
        }

        [Fact]

        public async Task Sholdreturn_OkLocalPut() 
        {
            // Arrange
            int id = 1;

            var existingLocal = new Local
            {
                Id = id,
                Endereco = "Endereço",
                Capacidade = 200

            };
            var localdto = new LocalDto()
            {
                Id = id,
                Endereco = existingLocal.Endereco,
                Capacidade = existingLocal.Capacidade
            };

            var local = new Local
            {
                Id = id,
                Endereco = localdto.Endereco,
                Capacidade = localdto.Capacidade
            };

            _localServices
                .Setup(x => x.GetLocalByIdAsync(id))
                .ReturnsAsync(local);

            _localServices
                .Setup(x => x.UpdateLocalAsync(It.IsAny<LocalDto>()))
                .ReturnsAsync(local);

            //Act

            var result = await _localcontroller.UpdateLocalAsync(id, localdto);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
            var returnedLocal = Assert.IsType<LocalDto>(okResult.Value);
            Assert.Equal(localdto.Id, returnedLocal.Id);
            Assert.Equal(localdto.Endereco, returnedLocal.Endereco);
            Assert.Equal(localdto.Capacidade, returnedLocal.Capacidade);





        }

    }
}
