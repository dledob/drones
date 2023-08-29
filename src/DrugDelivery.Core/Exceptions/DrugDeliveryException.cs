using System;

namespace DrugDelivery.Core.Exceptions;

public class DrugDeliveryException : Exception
{
    public string? ErrorCode { get; protected set; }
    public DrugDeliveryException(string message, string? status = null) : base(message)
    {
        ErrorCode = status;
    }

}
