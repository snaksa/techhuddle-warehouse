using System;

namespace TechhuddleWarehouse.Exceptions
{
    public class NotPositiveQuantityException : Exception
    {
        public NotPositiveQuantityException(string message) : base(message) { }
    }
}
