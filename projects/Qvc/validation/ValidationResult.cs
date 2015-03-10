using System.Collections.Generic;
using System.Linq;

using Qvc.Exception;

namespace Qvc.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
        }


        public ValidationResult(string message)
        {
            IsValid = false;
            Violations = new List<Violation>
            {
                new Violation
                {
                    FieldName = string.Empty,
                    Message = message
                }
            };
        }

        public ValidationResult(InvalidException validationException)
        {
            IsValid = false;
            Violations = new List<Violation>
            {
                new Violation
                {
                    FieldName = validationException.FieldName.ToCamelCase(),
                    Message = validationException.ErrorMessage
                }
            };
        }

        public ValidationResult(MultipleInvalidException validationExceptions)
        {
            IsValid = false;
            Violations = validationExceptions.InvalidExceptions.Select(x => new Violation
                                                                                {
                                                                                    Message = x.ErrorMessage,
                                                                                    FieldName = x.FieldName.ToCamelCase()
                                                                                });
        }

        public bool IsValid { get; set; }

        public IEnumerable<Violation> Violations { get; set; }


    }
}
