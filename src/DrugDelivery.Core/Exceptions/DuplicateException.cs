﻿using System;

namespace DrugDelivery.Core.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException(string message) : base(message)
    {

    }

}
