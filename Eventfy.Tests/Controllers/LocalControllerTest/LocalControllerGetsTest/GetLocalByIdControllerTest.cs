using Eventfy.Controllers;
using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Controllers.LocalControllerTest.LocalControllerGetsTest
{
    public class GetLocalByIdControllerTest
    {
        private readonly Mock<ILocalServices> _localServices;
        private readonly LocalController _localcontroller;
        public GetLocalByIdControllerTest() 
        {
            _localServices = new Mock<ILocalServices>();
            _localcontroller = new LocalController(_localServices.Object);
        }
        [Fact]

        public async Task ShouldReturn_OKLocalById()
        {
            var id = 1;

            var local = new Local
            {
                Id = id,
                Endereco = "Endereço",
                Capacidade = 100
            };

            _localServices
                .Setup(x => x.GetLocalByIdAsync(id))
                .ReturnsAsync(local);

            var result = await _localcontroller.GetLocalById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);


            var returnedLocal = Assert.IsType<Local>(okResult.Value);


            Assert.NotNull(result);
            Assert.Equal(1, local.Id);
            Assert.Equal("Endereço", returnedLocal.Endereco);
            Assert.Equal(100, returnedLocal.Capacidade);



        }
    }
}
