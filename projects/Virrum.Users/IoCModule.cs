namespace Virrum.Person
{
    using Autofac;

    using Virrum.Person.Contracts;

    public class IoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonService>().As<IPersonService>();
        }
    }

}