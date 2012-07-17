using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.Exceptions
{
    public class EstacionDBException : System.Exception
    {
        public EstacionDBException() { }
        public EstacionDBException(string message) : base (message) { }
        public EstacionDBException(string message, System.Exception inner) : base(message, inner) { }

        protected EstacionDBException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info,context)
        {
        }
       
    }
}
