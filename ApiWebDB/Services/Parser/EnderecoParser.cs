using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services.DTOs;
using APIWebDB.Services.DTOs;
using System;

namespace ApiWebDB.Services.Parser
{
    public class EnderecoParser
    {
        public static TbEndereco ToEntity(EnderecoDTO dto)
        {

            return new TbEndereco()
            {
                Bairro = dto.Bairro,    
                Cep = dto.Cep,
                Cidade = dto.Cidade,
                Complemento = dto.Complemento,
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                Status = dto.Status,
                Uf = dto.Uf,
                Clienteid = dto.Clienteid
            };
        }
    }
}
