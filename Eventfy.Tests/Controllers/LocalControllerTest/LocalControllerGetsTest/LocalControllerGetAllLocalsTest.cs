using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Eventfy.Tests.Controllers.LocalControllerTest.LocalControllerGetsTest
{
    public class LocalControllerGetAllLocalsTest
    {
        private readonly Mock<ILocalServices> _localServices;
        private readonly LocalController _localcontroller;
        public LocalControllerGetAllLocalsTest() 
        {
            _localServices = new Mock<ILocalServices>();
            _localcontroller = new LocalController(_localServices.Object);
        }
        [Fact]
        public async Task Shouldreturn_AllLocals()
        {
            //Assert
            var local = new List<Local>
            {
                new Local { Id = 1, Endereco = "Endereço teste", Capacidade = 100 },
                new Local { Id = 2, Endereco = "Endereço teste2", Capacidade = 200 }
            };
            

            _localServices
                .Setup(x => x.GetAllLocalsAsync())
                .ReturnsAsync(local);

            //Act

            var result = await _localcontroller.GetAllLocals();

            //Assert

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
            var returnedLocals = Assert.IsType<List<Local>>(okResult.Value);
            Assert.Equal(2, returnedLocals.Count);

            _localServices.Verify(x => x.GetAllLocalsAsync(), Times.Once);








        }
    }
}
