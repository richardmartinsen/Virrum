using System.Collections.Generic;

namespace Qvc.Validation.Metadata
{
    public class Parameter
    {
        public Parameter(string name)
        {
            Name = name;
            Constraints = new HashSet<ParameterConstraint>();
        }

        public string Name { get; private set; }

        public ISet<ParameterConstraint> Constraints { get; private set; }

        public void AddRule(ParameterConstraint rule)
        {
            Constraints.Add(rule);
        }
    }
}