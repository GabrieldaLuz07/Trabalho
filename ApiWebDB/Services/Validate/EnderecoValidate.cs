using ApiWebDB.Services.DTOs;
using ApiWebDB.Services.Exceptions;

namespace ApiWebDB.Services.Validate
{
    public class EnderecoValidate
    {
        public static bool Execute(EnderecoDTO dto)
        {
            if (dto.Cep.ToString().Length != 8 || dto.Cep < 0)
                throw new InvalidEntityExceptions("O CEP não é valido");

            if (string.IsNullOrEmpty(dto.Logradouro))
                throw new InvalidEntityExceptions("Campo Logradouro é obrigatório");

            if (string.IsNullOrEmpty(dto.Numero))
                throw new InvalidEntityExceptions("Campo Numero é obrigatório");

            if (string.IsNullOrEmpty(dto.Complemento))
                throw new InvalidEntityExceptions("Campo Complemento é obrigatório");

            if (string.IsNullOrEmpty(dto.Bairro))
                throw new InvalidEntityExceptions("Campo Bairro é obrigatório");

            if (string.IsNullOrEmpty(dto.Cidade))
                throw new InvalidEntityExceptions("Campo Cidade é obrigatório");

            if (string.IsNullOrEmpty(dto.Uf) || dto.Uf.ToString().Length > 2)
                throw new InvalidEntityExceptions("Campo Uf é obrigatório");

            if (int.IsNegative(dto.Clienteid) || dto.Clienteid == 0)
                throw new InvalidEntityExceptions("O id do cliente não é valido");

            if (int.IsNegative(dto.Status) || dto.Status > 1)
                throw new InvalidEntityExceptions("Status inválido");

            return true;
        }
    }
}
