using System;
using System.Runtime.Serialization;

namespace CommandLineManager
{
    [Serializable]
    internal class MissingCommandLineArgumentException : Exception
    {
        public MissingCommandLineArgumentException()
        {
        }

        public MissingCommandLineArgumentException(string message) : base(message)
        {
        }

        public MissingCommandLineArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingCommandLineArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}