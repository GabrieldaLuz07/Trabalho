﻿using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services;
using ApiWebDB.Services.Exceptions;
using APIWebDB.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ApiWebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Gerenciador de clientes
    /// </summary>
    public class ClientesController : ControllerBase
    {

        private readonly ClienteService _service;
        private readonly ILogger _logger;
        public ClientesController(ClienteService service, ILogger<ClientesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Inserir um novo cliente.
        /// </summary>
        /// <param name="cliente">Estrutura do cliente a ser inserido.
        /// É necessário informar todos os campos para criar um novo cliente.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 400= Requisição inválida;
        /// 422= Entidade inválida;</br></param>
        [HttpPost()]
        public ActionResult<TbCliente> Insert(ClienteDTO cliente)
        {
            try
            {
                var entity = _service.Insert(cliente);
                return Ok(entity);
            }
            catch (InvalidEntityExceptions E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 422
                };
            }
            catch (BadRequestException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 400
                };
            }
            catch (Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }
        }

        /// <summary>
        /// Atualizar um cliente existente.
        /// </summary>
        /// <param name="id">Identificador do cliente que será atualizado.</param>
        /// <param name="cliente">É necessário informar os campos que serão alterados do cliente.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 400= Requisição inválida;
        /// 404= Cliente não encontrado;
        /// 422= Cliente inválido;</br></param>
        [HttpPut("/cliente/put/{id}")]
        public ActionResult<TbCliente> Update(int id, ClienteDTO cliente)
        {
            try
            {
                var entity = _service.Update(cliente, id);
                return Ok(entity);
            }
            catch (InvalidEntityExceptions E)
            {
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 422
                };

            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (BadRequestException E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 400
                };
            }
            catch (Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }

        }

        /// <summary>
        /// Remove um cliente existente.
        /// </summary>
        /// <param name="id">Identificador do cliente que será removido.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 404= Cliente não encontrado;
        /// 500= Erro interno do servidor;</br></param>
        [HttpDelete("/cliente/delete/{id}")]
        public ActionResult<TbCliente> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (Exception E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 500
                };

            }
        }

        /// <summary>
        /// Buscar um cliente existente.
        /// </summary>
        /// <param name="id">Identificador do cliente que será buscado para exibição.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 404= Cliente não encontrado;
        /// 500= Erro interno do servidor;</br></param>
        [HttpGet("/cliente/getById/{id}")]
        public ActionResult<TbCliente> GetById(int id)
        {
            try
            {
                var entity = _service.GetById(id);
                return Ok(entity);
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (Exception E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 500
                };
            }
        }

        /// <summary>
        /// Buscar todos os cliente cadastrados.
        /// </summary>
        /// Os retornos são:
        /// 200= Sucesso;
        /// 404= Nenhum cliente cadastrado;
        /// 500= Erro interno do servidor;
        [HttpGet()]
        public ActionResult<TbCliente> Get()
        {
            try
            {
                var entity = _service.Get();
                return Ok(entity);
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (Exception E)
            {
                _logger.LogError(E.Message);
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 500
                };
            }
        }
    }
}