using System;

namespace DrugDelivery.Core.Exceptions;

public class ValidateModelException : DrugDeliveryException
{
    public ValidateModelException(string message, string? status = null) : base(message, status)
    {
    }

}
