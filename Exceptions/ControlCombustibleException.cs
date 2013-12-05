using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.Exceptions
{
    public class ControlCombustibleException : System.Exception
    {
        public ControlCombustibleException() { }
        public ControlCombustibleException(string message) : base (message) { }
        public ControlCombustibleException(string message, System.Exception inner) : base(message, inner) { }

        protected ControlCombustibleException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
