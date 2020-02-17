using System;

namespace TechhuddleWarehouse.Exceptions
{
    public class QuantityTooLowException : Exception
    {
        public QuantityTooLowException(string message) : base(message) { }
    }
}