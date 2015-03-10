using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qvc.Exception
{
    public class ExecutableDoesNotExistException : System.Exception
    {
        public ExecutableDoesNotExistException(string name) : base(name+" is not a Command or a Query")
        {
            
        }
    }
}
