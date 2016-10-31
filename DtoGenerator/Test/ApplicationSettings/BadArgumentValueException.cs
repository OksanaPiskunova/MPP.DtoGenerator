using System;
using System.Runtime.Serialization;

namespace Test.ApplicationSettings
{
    [Serializable]
    internal class BadArgumentValueException : Exception
    {
        public BadArgumentValueException()
        {
        }

        public BadArgumentValueException(string message) : base(message)
        {
        }

        public BadArgumentValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadArgumentValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}