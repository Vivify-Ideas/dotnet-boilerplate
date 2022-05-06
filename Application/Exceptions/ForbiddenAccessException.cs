using System;

namespace Application.Exceptions
{
    public class ForbiddenAccessException : ApplicationException
    {
        public ForbiddenAccessException(string message) : base(message)
        {

        }
    }
}
