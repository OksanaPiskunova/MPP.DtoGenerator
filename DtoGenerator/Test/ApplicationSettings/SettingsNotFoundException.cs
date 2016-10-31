using System;
using System.Runtime.Serialization;

namespace Test.ApplicationSettings
{
    [Serializable]
    internal class SettingsNotFoundException : Exception
    {
        public SettingsNotFoundException()
        {
        }

        public SettingsNotFoundException(string message) : base(message)
        {
        }

        public SettingsNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SettingsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}