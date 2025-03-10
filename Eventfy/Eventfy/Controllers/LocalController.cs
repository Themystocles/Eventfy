﻿using Eventfy.Interface.Interface_Services;
using Eventfy.Models;
using Eventfy.Models.DTOs;
using Eventfy.Service;
using Microsoft.AspNetCore.Mvc;

namespace Eventfy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly ILocalServices _localService;
        public LocalController(ILocalServices localService)
        {
            _localService = localService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetAllLocals()
        {
            var locals = await _localService.GetAllLocalsAsync();
            return Ok(locals.ToList());
        }
        [HttpGet("Local/{id}")]
        public async Task<ActionResult<Local>> GetLocalById(int id)
        {
            try
            {
                var local = await _localService.GetLocalByIdAsync(id);
                return Ok(local);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro Interno: {ex.Message}");
            }
            
            
        }
        [HttpPost("CreateLocal")]
        public async Task<ActionResult<Local>> AddLocalAsync(LocalDto localDto)
        {
            try 
            {
                var local = await _localService.CreateLocalAsync(localDto);
                return CreatedAtAction(nameof(GetLocalById), new { id = local.Id }, local);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro Interno: {ex.Message}");
            }
        }
        [HttpPut("UpdateLocal/{id}")]
        public async Task <ActionResult<Local>> UpdateLocalAsync(int id, [FromBody] LocalDto localDto)
        {
           
            var localdto = new LocalDto { Id = id, Endereco = localDto.Endereco, Capacidade = localDto.Capacidade };
            try
            {
                await _localService.UpdateLocalAsync(localdto);
                return Ok(localDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }

          
        }
        [HttpDelete("DeleteLocal/{id}")]
        public async Task <ActionResult<Local>> DeleteLocal(int id)
        {
            try
            {
                await _localService.DeleteLocal(id);
                return Ok();


            }
            catch
            {
                return StatusCode(500, $"Erro interno");
            }



        }





    }
}
