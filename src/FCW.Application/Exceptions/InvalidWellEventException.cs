using System;
using System.Collections.Generic;
using System.Text;

namespace FCW.Application.Exceptions;

public class InvalidWellEventException : Exception
{
    public InvalidWellEventException(string message) : base(message)
    {
    }
}
