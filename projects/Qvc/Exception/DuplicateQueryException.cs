using System.Collections.Generic;

namespace Qvc.Exception
{
    internal class DuplicateQueryException : System.Exception
    {
        public DuplicateQueryException(string queryName, IEnumerable<string> queries) :
            base("Query " + queryName + " is ambiguous, it could be one of " + string.Join(", ", queries))
        {
        }
    }
}