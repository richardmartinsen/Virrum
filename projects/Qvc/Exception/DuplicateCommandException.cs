using System.Collections.Generic;

namespace Qvc.Exception
{
    internal class DuplicateCommandException : System.Exception
    {
        public DuplicateCommandException(string commandName, IEnumerable<string> commands) :
            base("Command " + commandName + " is ambiguous, it could be one of " + string.Join(", ", commands))
        {
        }
    }
}
