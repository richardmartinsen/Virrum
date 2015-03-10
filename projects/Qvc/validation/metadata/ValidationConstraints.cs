using System.Collections.Generic;

namespace Qvc.Validation.Metadata
{
    public class ValidationConstraints
    {
        public ValidationConstraints()
        {
            Parameters = new HashSet<Parameter>();
            Exception = null;
        }

        public ValidationConstraints(System.Exception exception)
        {
            Exception = exception;
            Parameters = null;
        }

        public ISet<Parameter> Parameters { get; private set; }
        
        public System.Exception Exception { get; private set; }

        public void AddConstraint(Parameter constraint)
        {
            Parameters.Add(constraint);
        }
    }
}