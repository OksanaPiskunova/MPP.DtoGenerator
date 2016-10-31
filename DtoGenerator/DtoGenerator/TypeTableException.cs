using System;
using System.Runtime.Serialization;

namespace DtoGenerator
{
    [Serializable]
    public class TypeTableException : Exception
    {
        public TypeTableException()
        {
        }

        public TypeTableException(string message) : base(message)
        {
        }

        public TypeTableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeTableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}