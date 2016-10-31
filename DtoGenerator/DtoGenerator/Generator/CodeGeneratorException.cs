using System;
using System.Runtime.Serialization;

namespace DtoGenerator.Generator
{
    [Serializable]
    public class CodeGeneratorException : Exception
    {
        public CodeGeneratorException()
        {
        }

        public CodeGeneratorException(string message) : base(message)
        {
        }

        public CodeGeneratorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CodeGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}