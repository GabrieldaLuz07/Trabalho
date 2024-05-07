using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services.Exceptions;
using ApiWebDB.Services.Parser;
using ApiWebDB.Services.Validate;
using APIWebDB.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiWebDB.Services
{
    public class ClienteService
    {

        private readonly ApidbContext _dbcontext;

        public ClienteService(ApidbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public TbCliente Insert(ClienteDTO dto)
        {
            if (!ClienteValidate.Execute(dto))
                return null;

            var entity = ClienteParser.ToEntity(dto);

            _dbcontext.Add(entity);
            _dbcontext.SaveChanges();

            return entity;
        }

        public TbCliente Update(ClienteDTO dto, int id)
        {
            if (!ClienteValidate.Execute(dto))
                return null;

            var ClienteById = GetById(id);
            if (ClienteById == null)
                throw new NotFoundException("Registro não existe");

            var entity = ClienteParser.ToEntity(dto);

            ClienteById.Nome = entity.Nome;
            ClienteById.Nascimento = entity.Nascimento;
            ClienteById.Telefone = entity.Telefone;
            ClienteById.Documento = entity.Documento;
            ClienteById.Tipodoc = entity.Tipodoc;
            ClienteById.Alteradoem = DateTime.Now;

            _dbcontext.Update(ClienteById);
            _dbcontext.SaveChanges();

            return entity;
        }

        public TbCliente GetById(int id)
        {
            var existingEntity = _dbcontext.TbClientes.FirstOrDefault(c => c.Id == id);
            if (existingEntity == null)
                throw new NotFoundException("Registro não existe");

            return existingEntity;
        }

        public IEnumerable<TbCliente> Get()
        {
            var existingEntity = _dbcontext.TbClientes.ToList();
            if (existingEntity == null || existingEntity.Count == 0)
                throw new NotFoundException("Nenhum registro encontrado");

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