using Eventfy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Models
{
    public class LocalTest
    {
        [Fact]
        public void CreateLocalsuccess()
        {
            //Arrange
            var endereco = "Rua Natal 333";
            var capacidade = 40;
            var IdEvent = 1;
            var eventMock = new Event(1, "Palestra Pablo Marçal", "Palestra Pablo Marçal", DateTime.Now);

      
            //Act

            var local = new Local(1, endereco, capacidade, eventMock, IdEvent);

            //Assert
            Assert.NotNull(local); 
            Assert.Equal(endereco, local.Endereco); 
            Assert.Equal(capacidade, local.Capacidade);
            Assert.Equal(IdEvent, local.IdEvent); 
            Assert.Equal(eventMock, local.Event); 

        }
    }
}
