using Eventfy.Models;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;

namespace Eventfy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly LocalService _localService;
        public LocalController(LocalService localService)
        {
            _localService = localService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetAllLocals() 
        {
            var locals = await _localService.GetAllLocalsAsync();
            return locals.ToList();
        }
        

    }
}
