using System;

namespace TechhuddleWarehouse.Exceptions
{
    public class QuantityTooHighException : Exception
    {
        public QuantityTooHighException(string message) : base(message) { }
    }
}