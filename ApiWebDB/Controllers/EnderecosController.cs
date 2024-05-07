using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services;
using ApiWebDB.Services.DTOs;
using ApiWebDB.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ApiWebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Gerenciador de endereços
    /// </summary>
    public class EnderecosController : ControllerBase
    {
        private readonly EnderecoService _service;
        private readonly ILogger _logger;
        public EnderecosController(EnderecoService service, ILogger<ClientesController> logger)
        {
            _service = service;
        }

        /// <summary>
        /// Inserir um novo endereço.
        /// </summary>
        /// <param name="endereco">Estrutura do endereço a ser inserido.
        /// É necessário informar todos os campos para criar um novo endereço.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 400= Requisição inválida;
        /// 422= Entidade inválida;</br></param>
        [HttpPost()]
        public ActionResult<TbEndereco> Insert(EnderecoDTO endereco)
        {
            try
            {
                var entity = _service.Insert(endereco);
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
        /// Atualizar um endereço existente.
        /// </summary>
        /// <param name="id">Identificador do endereço que será atualizado.</param>
        /// <param name="endereco">É necessário informar os campos que serão alterados do endereço.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 400= Requisição inválida;
        /// 404= Endereço não encontrado;
        /// 422= Endereço inválido;</br></param>
        [HttpPut("/endereco/put/{id}")]
        public ActionResult<TbEndereco> Update(int id, EnderecoDTO endereco)
        {
            try
            {
                var entity = _service.Update(endereco, id);
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
        /// Remove um endereço existente.
        /// </summary>
        /// <param name="id">Identificador do endereço que será removido.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 404= Endereço não encontrado;
        /// 500= Erro interno do servidor;</br></param>
        [HttpDelete("/endereco/delete/{id}")]
        public ActionResult<TbEndereco> Delete(int id)
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
        /// Buscar um endereço existente.
        /// </summary>
        /// <param name="id">Identificador do endereço que será buscado para exibição.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 404= Endereço não encontrado;
        /// 500= Erro interno do servidor;</br></param>
        [HttpGet("/endereco/getById/{id}")]
        public ActionResult<TbEndereco> GetById(int id)
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
        /// Buscar todos os endereços de um cliente.
        /// </summary>
        /// <param name="idCliente">Identificador do cliente.
        /// <br>Os retornos são:
        /// 200= Sucesso;
        /// 404= Cliente não encontrado;
        /// 500= Erro interno do servidor;</br></param>
        [HttpGet("/endereco/getByIdCliente/{idCliente}")]
        public ActionResult<TbEndereco> Get(int idCliente)
        {
            try
            {
                var listaEnderecos = _service.GetByIdCliente(idCliente);
                return Ok(listaEnderecos);
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
