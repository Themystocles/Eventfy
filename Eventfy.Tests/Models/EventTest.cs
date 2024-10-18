using Eventfy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventfy.Tests.Models
{
  
   public class EventTest
    {
        [Fact]
        
        public void CreateEventsuccess()
        {
            //Arrange
            var name = "name";
            var description = "description";
            var dateEvent = DateTime.Now;
            var eventParticipant = new  List<string>() {"test", "teste" };

            //Act
            var evento = new Event(0, name, description, dateEvent);

            //Assert
            Assert.Equal(evento.Name, name);
            Assert.Equal(evento.Description, description);
            Assert.Equal(evento.DateEvent, dateEvent);
        }



    }
}
