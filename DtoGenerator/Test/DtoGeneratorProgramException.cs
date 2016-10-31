using System;
using System.Runtime.Serialization;

namespace Test
{
    [Serializable]
    internal class DtoGeneratorProgramException : Exception
    {
        public DtoGeneratorProgramException()
        {
        }

        public DtoGeneratorProgramException(string message) : base(message)
        {
        }

        public DtoGeneratorProgramException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DtoGeneratorProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}