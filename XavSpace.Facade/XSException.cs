using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Facade
{
    [Serializable]
    public class XSException : Exception
    {
        public XSException() { }
        public XSException(string message) : base(message) { }
        public XSException(string message, Exception inner) : base(message, inner) { }
        protected XSException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
