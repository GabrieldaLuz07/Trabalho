using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services.DTOs;
using ApiWebDB.Services.Exceptions;
using ApiWebDB.Services.Parser;
using ApiWebDB.Services.Validate;
using System.Collections.Generic;
using System.Linq;

namespace ApiWebDB.Services
{
    public class EnderecoService
    {
        private readonly ApidbContext _dbcontext;

        public EnderecoService(ApidbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public TbEndereco Insert(EnderecoDTO dto)
        {
            if (!EnderecoValidate.Execute(dto))
                return null;

            var entity = EnderecoParser.ToEntity(dto);

            _dbcontext.Add(entity);
            _dbcontext.SaveChanges();

            return entity;
        }

        public TbEndereco Update(EnderecoDTO dto, int id)
        {
            if (!EnderecoValidate.Execute(dto))
                return null;

            var existingEntity = GetById(id);

            if (existingEntity == null)
                throw new NotFoundException("Registro não existe");

            var entity = EnderecoParser.ToEntity(dto);

            var EnderecoById = GetById(id);

            EnderecoById.Uf = entity.Uf;
            EnderecoById.Cliente = entity.Cliente;
            EnderecoById.Cep = entity.Cep;
            EnderecoById.Logradouro = entity.Logradouro;
            EnderecoById.Status = entity.Status;
            EnderecoById.Numero = entity.Numero;
            EnderecoById.Bairro = entity.Bairro;
            EnderecoById.Cidade = entity.Cidade;
            EnderecoById.Complemento = entity.Complemento;

            _dbcontext.Update(EnderecoById);
            _dbcontext.SaveChanges();

            return entity;
        }

        public TbEndereco Put(EnderecoDTO dto, int id)
        {
            if (!EnderecoValidate.Execute(dto))
                return null;


            var entity = EnderecoParser.ToEntity(dto);

            var EnderecoById = GetById(id);

            EnderecoById.Uf = entity.Uf;
            EnderecoById.Clienteid = entity.Clienteid;
            EnderecoById.Cep = entity.Cep;
            EnderecoById.Logradouro = entity.Logradouro;
            EnderecoById.Status = entity.Status;
            EnderecoById.Numero = entity.Numero;
            EnderecoById.Bairro = entity.Bairro;
            EnderecoById.Cidade = entity.Cidade;
            EnderecoById.Complemento = entity.Complemento;

            _dbcontext.Update(EnderecoById);
            _dbcontext.SaveChanges();

            return EnderecoById;

        }

        public TbEndereco GetById(int id)
        {
            var existingEntity = _dbcontext.TbEnderecos.FirstOrDefault(e => e.Id == id);
            if (existingEntity == null)
                throw new NotFoundException("Registro não existe");

            return existingEntity;
        }

        public IEnumerable<TbEndereco> GetByIdCliente(int idCliente)
        {
            var existingEntity = _dbcontext.TbEnderecos.Where(e => e.Clienteid == idCliente).ToList();
            if (existingEntity == null || existingEntity.Count == 0)
                throw new NotFoundException("O cliente não possui nenhum endereço cadastrado");

            return existingEntity;
        }

        public void Delete(int id)
        {
            var existingEntity = GetById(id);

            if (existingEntity == null)
                throw new NotFoundException("Registro não existe");

            _dbcontext.Remove(existingEntity);
            _dbcontext.SaveChanges();

        }
    }
}
