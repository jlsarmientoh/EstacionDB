using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.Exceptions
{
    public class CierreException : System.Exception
    {
        public CierreException() { }
        public CierreException(string message) : base (message) { }
        public CierreException(string message, System.Exception inner) : base(message, inner) { }

        protected CierreException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
