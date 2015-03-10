using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Qvc.Attributes;
using Qvc.Executable;
using Qvc.Validation.Metadata;

namespace Qvc.Validation
{
    internal class ExecutableValidator
    {
        public ValidationResult Validate(IExecutable executable)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var path = new List<string>();

            var isValid = ValidateRecursively(executable, results, path);

            var validationResult = new ValidationResult
                                       {
                                           IsValid = isValid, 
                                           Violations =
                                               results.Select(
                                                   violation =>
                                                   new Violation
                                                       {
                                                           FieldName = string.Join(".", violation.MemberNames.Select(x => x.ToCamelCase())), 
                                                           Message = violation.ErrorMessage
                                                       })
                                       };

            return validationResult;
        }

        public ValidationConstraints ValidationConstraints(Type objectWithConstraints)
        {
            var validationConstraints = new ValidationConstraints();

            ValidationConstraintsRecursively(objectWithConstraints, validationConstraints, new string[] { }.ToList());

            return validationConstraints;
        }

        private void ValidationConstraintsRecursively(Type objectWithConstraints, ValidationConstraints validationConstraints, List<string> path)
        {
            foreach (var propertyDescriptor in objectWithConstraints.GetProperties())
            {
                var name = propertyDescriptor.Name.ToCamelCase();
                var newPath = AppendToList(path, name);
                var constraint = new Parameter(string.Join(".", newPath));

                foreach (var constraintDescriptor in GetvalidationConstraintsForProperty(propertyDescriptor))
                {
                    var message = MessageFromAttribute((ValidationAttribute)constraintDescriptor);
                    var rule = ConstraintForValidationAttribute((dynamic)constraintDescriptor, message);
                    constraint.AddRule(rule);
                }

                validationConstraints.AddConstraint(constraint);
            }

            FindAllPropertiesWithValidateAttribute(objectWithConstraints).ToList().ForEach(x => ValidationConstraintsRecursively(x.PropertyType, validationConstraints, AppendToList(path, x.Name.ToCamelCase()).ToList()));
        }

        private static IEnumerable<T> AppendToList<T>(IEnumerable<T> path, T name)
        {
            return path.Concat(new[] { name });
        }

        private static IEnumerable<Attribute> GetvalidationConstraintsForProperty(PropertyInfo propertyDescriptor)
        {
            return Attribute.GetCustomAttributes(propertyDescriptor).Where(t => typeof(ValidationAttribute).IsAssignableFrom(t.GetType()));
        }

        private static bool ValidateRecursively(object objectToValidate, List<System.ComponentModel.DataAnnotations.ValidationResult> results, IEnumerable<string> path)
        {
            var context = new ValidationContext(objectToValidate, serviceProvider: null, items: null);
            var currentLevelResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(objectToValidate, context, currentLevelResults, true);
            results.AddRange(currentLevelResults.Select(x => new System.ComponentModel.DataAnnotations.ValidationResult(x.ErrorMessage, path.Concat(x.MemberNames))));

            var propertiesToValidate = FindAllPropertiesWithValidateAttribute(objectToValidate.GetType()).ToList();

            return isValid && propertiesToValidate.All(x => ValidateRecursively(x.GetValue(objectToValidate), results, AppendToList(path, x.Name)));
        }

        private static IEnumerable<PropertyInfo> FindAllPropertiesWithValidateAttribute(Type type)
        {
            return type.GetProperties().Where(x => Attribute.GetCustomAttributes(x).Any(a => a is ValidateAttribute));
        }

        private ParameterConstraint ConstraintForValidationAttribute(RequiredAttribute attribute, string message)
        {
            return new ParameterConstraint("NotEmpty", new { Message = message.Replace("{0}", "{this.name}") });
        }

        private ParameterConstraint ConstraintForValidationAttribute(RangeAttribute attribute, string message)
        {
            return new ParameterConstraint("Range", new { Message = message.Replace("{0}", "{this.name}").Replace("{1}", "{min}").Replace("{2}", "{max}"), Min = attribute.Minimum, Max = attribute.Maximum });
        }

        private ParameterConstraint ConstraintForValidationAttribute(RegularExpressionAttribute attribute, string message)
        {
            return new ParameterConstraint("Pattern", new { Message = message.Replace("{0}", "{this.name}"), Regexp = attribute.Pattern });
        }

        private ParameterConstraint ConstraintForValidationAttribute(Attribute attribute, string message)
        {
            return new ParameterConstraint(attribute.GetType().Name, new { Message = message });
        }

        private string MessageFromAttribute(ValidationAttribute attribute)
        {
            var errorMessage = (string)attribute.GetType().GetProperty("ErrorMessage").GetValue(attribute);
            var resourceType = (TypeInfo)attribute.GetType().GetProperty("ErrorMessageResourceType").GetValue(attribute);
            var name = (string)attribute.GetType().GetProperty("ErrorMessageResourceName").GetValue(attribute);

            if (errorMessage != null)
            {
                return errorMessage;
            }

            if (resourceType != null && name != null)
            {
                return (string)resourceType.GetProperty(name, BindingFlags.Public | BindingFlags.Static).GetValue(null);
            }

            return "Ugyldig verdi";
        }
    }
}