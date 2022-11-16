using System;

namespace StadiumEngine.Entities.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
        
    }
}