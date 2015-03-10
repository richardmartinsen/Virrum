namespace Qvc.Validation.Metadata
{
    public class ParameterConstraint
    {
        public ParameterConstraint(string name, dynamic attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        public string Name { get; set; }

        public dynamic Attributes { get; set; }
    }
}