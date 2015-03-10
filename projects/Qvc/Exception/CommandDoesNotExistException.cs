using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qvc.Exception
{
    public class CommandDoesNotExistException : System.Exception
    {
        public CommandDoesNotExistException(string name) :
            base("Command " + name + " does not exist")
        {
            
        }
    }
}
