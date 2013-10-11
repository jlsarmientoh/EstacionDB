using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.Exceptions
{
    public class UsuarioException : System.Exception
    {
        public UsuarioException() { }
        public UsuarioException(string message) : base (message) { }
        public UsuarioException(string message, System.Exception inner) : base(message, inner) { }

        protected UsuarioException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
