using System;
using System.Runtime.Serialization;

namespace llvm_test.Parsing.Scopes
{
    [Serializable]
    internal class NamedVariableNotFoundException : Exception
    {
        public NamedVariableNotFoundException()
        {
        }

        public NamedVariableNotFoundException(string message) : base(message)
        {
        }

        public NamedVariableNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NamedVariableNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}