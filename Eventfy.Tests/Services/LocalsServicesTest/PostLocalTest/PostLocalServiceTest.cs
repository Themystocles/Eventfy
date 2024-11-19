﻿using Eventfy.Interface;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Services.LocalsServicesTest.PostLocalTest
{   
    public class PostLocalServiceTest
    {
        private readonly Mock<ILocalPersist> _MocklocalPersist;
        public PostLocalServiceTest()
        {
            _MocklocalPersist = new Mock<ILocalPersist>();
        }

        [Fact]
        public async Task ShouldReturnNewLocalCreate()
        {
            //Arrange
            var LocalFakeDto = new LocalDto()
            {
                Id = 1,
                Endereco = "Arena Castelão",
                Capacidade = 100000

            };
            var CreatedlocalFake = new Local()
            {
                Id = LocalFakeDto.Id,
                Capacidade = LocalFakeDto.Capacidade,
                Endereco = LocalFakeDto.Endereco
            };

            _MocklocalPersist
                .Setup(repo => repo.CreateLocalAsync(CreatedlocalFake))
                .ReturnsAsync(CreatedlocalFake);

            
            var LocalService = new LocalService(_MocklocalPersist.Object);

            //act
            var result =  await LocalService.CreateLocalAsync(LocalFakeDto);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(CreatedlocalFake.Endereco, result.Endereco);
            Assert.Equal(CreatedlocalFake.Capacidade, result.Capacidade);
           

        }

    }
}
