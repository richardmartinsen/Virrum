using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qvc.Exception
{
    class QueryDoesNotExistException : System.Exception
    {
        public QueryDoesNotExistException(string name) :
            base("Query " + name + " does not exist")
        {
            
        }
    }
}
