using System;

namespace ApiWebDB.Services.Exceptions
{
    public class InvalidEntityExceptions : Exception
    {

        public InvalidEntityExceptions(string message) : base(message)
        {

        }

    }
}