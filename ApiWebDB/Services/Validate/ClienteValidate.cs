using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services.Exceptions;
using APIWebDB.Services.DTOs;

namespace ApiWebDB.Services.Validate
{
    public class ClienteValidate
    {
        private static bool ValidateDocument(
            TipoDocumento tipo, string documento)
        {
            switch (tipo)
            {
                case TipoDocumento.CPF:
                    if (documento.Length != 11)
                        throw new BadRequestException(
                            "O CPF precisa ter 11 digitos");
                    return true;
                case TipoDocumento.CNPJ:
                    if (documento.Length != 14)
                        throw new BadRequestException(
                            "O CNPJ precisa ter 14 digitos");
                    return true;
                case TipoDocumento.Passaporte:
                    if (documento.Length != 8)
                        throw new BadRequestException(
                            "O Passaporte precisa ter 8 digitos");
                    return true;
                case TipoDocumento.CNH:
                    if (documento.Length != 11)
                        throw new BadRequestException(
                            "A CNH precisa ter 11 digitos");
                    return true;
                default:
                    return true;
            }
        }
        public static bool Execute(ClienteDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Nome))
                throw new InvalidEntityExceptions("Campo Nome é obrigatório");

            if (string.IsNullOrEmpty(dto.Documento))
                throw new InvalidEntityExceptions("Campo Documento é obrigatório");

            TipoDocumento tipo;
            try
            {
                tipo = (TipoDocumento)dto.Tipodoc;
            } 
            catch
            {
                throw new InvalidEntityExceptions(
                    $"O TipoDoc {dto.Tipodoc} é invalido.");
            }
            return ValidateDocument(tipo, dto.Documento);
        }
    }
}