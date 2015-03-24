namespace Virrum.Web.App_Start
{
    using Autofac;

    using Virrum.Data;
    using Virrum.Data.Contracts;
    using Virrum.Data.Extensions;
    using Virrum.Person;
    using Virrum.Person.Contracts;

    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonService>().As<IPersonService>();
            builder.RegisterType<VirrumDbProvider>().As<IVirrumDbProvider>();
            builder.RegisterType<SystemTime>().As<ISystemTime>().SingleInstance();
        }
    }
}