using System;

namespace DrugDelivery.Core.Exceptions;

public class DrugDeliveryException : Exception
{
    public int? SubStatusCode { get; protected set; }
    public DrugDeliveryException(string message, int? status = null) : base(message)
    {
        SubStatusCode = status;
    }

}
