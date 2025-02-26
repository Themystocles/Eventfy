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

namespace Eventfy.Tests.Controllers.LocalControllerTest.LocalControllerDeleteTest
{
    public class LocalControllerDeleteTest
    {

        private readonly Mock<ILocalServices> _localServices;
        private readonly LocalController _localcontroller;

        public LocalControllerDeleteTest()
        {
            _localServices = new Mock<ILocalServices>();
            _localcontroller = new LocalController(_localServices.Object);
        }
        [Fact]

        public async Task Shouldreturn_Ok_when_LocalDeleted() 
        {
            var id = 1;
            var existingLocal = new Local
            {
                Id = id,
                Endereco = "Endereço",
                Capacidade = 200
            };

            _localServices
                .Setup(x => x.GetLocalByIdAsync(1))
                .ReturnsAsync(existingLocal);

            _localServices
                .Setup(x => x.DeleteLocal(id))
                .ReturnsAsync(true);

            var result = await _localcontroller.DeleteLocal(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);


        }
    }
}
