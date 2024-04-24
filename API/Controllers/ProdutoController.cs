using API.Database.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoServices _produtoServices;
        public ProdutoController(ProdutoServices produtoServices) 
        {
            _produtoServices = produtoServices;
        }

        [HttpGet()]
        public ActionResult<List<Produto>> GetAll()
        {
            return Ok(_produtoServices.GetAll());
        }

        [HttpGet(":codigo")]
        public ActionResult<Produto> GetByCodigo(int codigo) 
        {
            try
            {
                var produto = _produtoServices.GetById(codigo);

                return Ok(produto);
            }
            catch (NotFoundException)
            {
                return NotFound("Produto não encontrado.");
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um problema ao acessar o produto. " + e.Message);
            }
        }
    }
}
