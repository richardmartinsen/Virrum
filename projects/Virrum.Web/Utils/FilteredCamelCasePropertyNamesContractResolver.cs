namespace Virrum.Web.Utils
{
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Qvc;

    public class FilteredCamelCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            if (!member.DeclaringType.Assembly.FullName.Contains("Microsoft"))
            {
                jsonProperty.PropertyName = jsonProperty.PropertyName.ToCamelCase();
            }

            return jsonProperty;
        }
    }
}