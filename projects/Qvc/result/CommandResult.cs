using System.Collections.Generic;

using Qvc.Validation;

namespace Qvc.Result
{
    public class CommandResult
    {
        public CommandResult(System.Exception exception)
        {
            Exception = exception;
            Success = false;
            Valid = false;
            Violations = null;
        }

        public CommandResult(ValidationResult validationResult)
        {
            Valid = validationResult.IsValid;
            Exception = null;
            Success = Valid;
            Violations = validationResult.Violations;
        }

        public CommandResult()
        {
            Success = true;
            Exception = null;
            Valid = true;
            Violations = null;
        }

        public bool Success { get; private set; }

        public bool Valid { get; private set; }

        public System.Exception Exception { get; private set; }

        public IEnumerable<Violation> Violations { get; private set; }
    }
}