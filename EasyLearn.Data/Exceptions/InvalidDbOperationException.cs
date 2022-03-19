using System;

namespace EasyLearn.Data.Exceptions
{
    public class InvalidDbOperationException : Exception
    {
        public InvalidDbOperationException(string message) : base(message) { }
    }
}
