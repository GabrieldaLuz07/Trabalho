using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services;
using ApiWebDB.Services.Exceptions;
using ApiWebDB.Services.Parser;
using APIWebDB.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiWebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly ClienteService _service;
        public ClientesController(ClienteService service)
        {
            _service = service;
        }

        [HttpPost()]
        public ActionResult<TbCliente> Insert(ClienteDTO cliente)
        {
            try
            {
                var entity = _service.Insert(cliente);
                return Ok(entity);
            }
            catch (Exception E)
            {
                return BadRequest(E.Message);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult<TbCliente> UpdatePatch(int id, ClienteDTO cliente)
        {
            try
            {
                var existingEntity = _service.GetById(id);
                if (existingEntity == null)
                {
                    return NotFound();
                }

                ClienteParser.UpdateEntityFromDTO(cliente, existingEntity);

                var updatedEntity = _service.Update(existingEntity);
                return Ok(updatedEntity);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TbCliente> UpdatePut(int id, ClienteDTO cliente)
        {
            try
            {
                var existingEntity = _service.GetById(id);
                if (existingEntity == null)
                {
                    return NotFound();
                }

                ClienteParser.UpdateEntityFromDTO(cliente, existingEntity);

                var updatedEntity = _service.Update(existingEntity);
                return Ok(updatedEntity);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}