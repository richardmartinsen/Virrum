using System.Collections.Generic;

using Qvc.Validation;

namespace Qvc.Result
{
    public class QueryResult
    {
        public QueryResult(dynamic result)
        {
            Success = true;
            Valid = true;
            Result = result;
            Exception = null;
            Violations = null;
        }

        public QueryResult(ValidationResult validationResult)
        {
            Valid = validationResult.IsValid;
            Exception = null;
            Success = Valid;
            Violations = validationResult.Violations;
            Result = null;
        }

        public QueryResult(System.Exception exception)
        {
            Exception = exception;
            Success = false;
            Valid = false;
            Result = null;
            Violations = null;
        }

        public QueryResult()
        {
            Success = true;
            Exception = null;
            Valid = false;
            Violations = null;
            Result = null;
        }

        public bool Success { get; private set; }

        public bool Valid { get; private set; }

        public System.Exception Exception { get; private set; }

        public IEnumerable<Violation> Violations { get; private set; }

        public dynamic Result { get; private set; }
    }
}