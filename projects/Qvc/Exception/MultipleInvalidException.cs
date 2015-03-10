using System.Collections.Generic;

namespace Qvc.Exception
{
    public class MultipleInvalidException : System.Exception
    {
        public MultipleInvalidException(IEnumerable<InvalidException> exceptions)
        {
            InvalidExceptions = exceptions;
        }

        public IEnumerable<InvalidException> InvalidExceptions { get; set; }
    }
}