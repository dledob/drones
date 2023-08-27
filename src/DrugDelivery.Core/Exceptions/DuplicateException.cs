using System;

namespace DrugDelivery.Core.Exceptions;

public class DuplicateException : DrugDeliveryException
{
    public DuplicateException(string message) : base(message)
    {
    }

}
