using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.Exceptions
{
    public class PersistenciaException : System.Exception
    {
        public PersistenciaException() { }
        public PersistenciaException(string message) : base(message) { }
        public PersistenciaException(string message, System.Exception inner) : base(message, inner) { }

        protected PersistenciaException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

    }
}
